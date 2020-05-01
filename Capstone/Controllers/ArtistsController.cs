using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Capstone.Data;
using Capstone.Models;
using System.Security.Claims;
using Capstone.Contracts;
using Capstone.Services;
using Newtonsoft.Json;

namespace Capstone.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILocationService _locationService;

        public ArtistsController(ApplicationDbContext context, ILocationService locationService)
        {
            _context = context;
            _locationService = locationService;
        }

        // GET: Artists
        public async Task<IActionResult> Index()
        {
            var artistId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var artist = _context.Artist.Where(a => a.IdentityUserId == artistId).FirstOrDefault();
            ViewBag.CurrentArtistId = artist.ArtistId;
            var numberOfRequests = _context.ConsumerRequest.Count();
            ViewBag.NumberOfRequests = numberOfRequests;
            ViewBag.ImgUrl = artist.ImgUrl;
            var orders = _context.ArtworkOrder.Where(b => b.ArtistId == artist.ArtistId).ToList();
            var profit = GetTotalProfit();
            ViewBag.TotalProfit = profit;
            var artworksSold = orders.Count();
            List<DataPoint> dataPoints1 = new List<DataPoint>();
            List<DataPoint> dataPoints2 = new List<DataPoint>();
            List<DataPoint> dataPoints3 = new List<DataPoint>();
            dataPoints1.Add(new DataPoint("2019", 5));
            dataPoints1.Add(new DataPoint("2020", artworksSold));
            dataPoints2.Add(new DataPoint("Mar", 7));
            dataPoints2.Add(new DataPoint("Apr", 2));
            dataPoints2.Add(new DataPoint("May", numberOfRequests));
            dataPoints3.Add(new DataPoint("Mar", 25));
            dataPoints3.Add(new DataPoint("Apr", 65));
            dataPoints3.Add(new DataPoint("May", profit));
            ViewBag.Data1 = JsonConvert.SerializeObject(dataPoints1);
            ViewBag.Data2 = JsonConvert.SerializeObject(dataPoints2);
            ViewBag.Data3 = JsonConvert.SerializeObject(dataPoints3);
            var applicationDbContext = _context.ConsumerRequest.Select(a=>a);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Artists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Consumer
                .Include(a => a.IdentityUser)
                .FirstOrDefaultAsync(m => m.ConsumerId == id);
            var artistId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var artist = _context.Artist.Where(a => a.IdentityUserId == artistId).FirstOrDefault();
            ViewBag.CurrentArtistId = artist.ArtistId;

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Artists/Create
        public IActionResult Create()
        {
            
            List<SelectListItem> categories = new List<SelectListItem>();
            categories.Add(new SelectListItem { Value = "Painting", Text = "Painting" });
            categories.Add(new SelectListItem { Value = "Digital", Text = "Digital" });
            categories.Add(new SelectListItem { Value = "Drawing", Text = "Drawing" });
            categories.Add(new SelectListItem { Value = "Sculpture", Text = "Sculpture" });
            categories.Add(new SelectListItem { Value = "Clothing", Text = "Clothing" });
            categories.Add(new SelectListItem { Value = "Crafts", Text = "Crafts" });
            categories.Add(new SelectListItem { Value = "Woodwork", Text = "Woodwork" });
            categories.Add(new SelectListItem { Value = "Jewelry", Text = "Jewelry" });
            categories.Add(new SelectListItem { Value = "Photography", Text = "Photography" });
            ViewBag.ArtCategories = categories;
            Artist artist = new Artist();
            
            return View(artist);
        }

        // POST: Artists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArtistId,FirstName,LastName,Email,ImgUrl,ArtistCategories")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                artist.IdentityUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(artist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", artist.IdentityUserId);
            return View(artist);
        }

        // GET: Artists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artist.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", artist.IdentityUserId);
            return View(artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArtistId,FirstName,LastName,Email,ImgUrl,IdentityUserId")] Artist artist)
        {
            if (id != artist.ArtistId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistExists(artist.ArtistId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", artist.IdentityUserId);
            return View(artist);
        }

        // GET: Artists/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artist
                .Include(a => a.IdentityUser)
                .FirstOrDefaultAsync(m => m.ArtistId == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artist = await _context.Artist.FindAsync(id);
            _context.Artist.Remove(artist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistExists(int id)
        {
            return _context.Artist.Any(e => e.ArtistId == id);
        }

        public double GetTotalProfit()
        {
            double totalProfit = 0;
            var artistId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var artist = _context.Artist.Where(a => a.IdentityUserId == artistId).FirstOrDefault();
            var orders = _context.ArtworkOrder.Where(b => b.ArtistId == artist.ArtistId).ToList();
            foreach(var order in orders)
            {
                totalProfit += order.ArtistArtwork.ArtworkPrice;
            }
            return totalProfit;
        }
    }
}
