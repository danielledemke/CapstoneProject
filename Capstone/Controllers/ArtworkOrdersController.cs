using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Capstone.Data;
using Capstone.Models;

namespace Capstone.Controllers
{
    public class ArtworkOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtworkOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ArtworkOrders
        public ActionResult Index()
        {
           
                return RedirectToAction("Index", "Artists");
            
        }
         public ActionResult GetAllOrders(int id)
        {
            var orders = _context.ArtworkOrder.Where(s => s.ArtistId == id);
            return View(orders);
        }

        // GET: ArtworkOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artworkOrder = await _context.ArtworkOrder
                .Include(a => a.Artist)
                .Include(a => a.ArtistArtwork)
                .Include(a => a.Consumer)
                .FirstOrDefaultAsync(m => m.ArtworkOrderId == id);
            if (artworkOrder == null)
            {
                return NotFound();
            }

            return View(artworkOrder);
        }

        // GET: ArtworkOrders/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artist, "ArtistId", "ArtistId");
            ViewData["ArtistArtworkId"] = new SelectList(_context.ArtistArtwork, "ArtistArtworkId", "ArtistArtworkId");
            ViewData["ConsumerId"] = new SelectList(_context.Consumer, "ConsumerId", "ConsumerId");
            return View();
        }

        // POST: ArtworkOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArtworkOrderId,PurchaseDate,ShippedDate,ArtistArtworkId,ArtistId,ConsumerId")] ArtworkOrder artworkOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artworkOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "ArtistId", "ArtistId", artworkOrder.ArtistId);
            ViewData["ArtistArtworkId"] = new SelectList(_context.ArtistArtwork, "ArtistArtworkId", "ArtistArtworkId", artworkOrder.ArtistArtworkId);
            ViewData["ConsumerId"] = new SelectList(_context.Consumer, "ConsumerId", "ConsumerId", artworkOrder.ConsumerId);
            return View(artworkOrder);
        }

        // GET: ArtworkOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artworkOrder = await _context.ArtworkOrder.FindAsync(id);
            if (artworkOrder == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "ArtistId", "ArtistId", artworkOrder.ArtistId);
            ViewData["ArtistArtworkId"] = new SelectList(_context.ArtistArtwork, "ArtistArtworkId", "ArtistArtworkId", artworkOrder.ArtistArtworkId);
            ViewData["ConsumerId"] = new SelectList(_context.Consumer, "ConsumerId", "ConsumerId", artworkOrder.ConsumerId);
            return View(artworkOrder);
        }

        // POST: ArtworkOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArtworkOrderId,PurchaseDate,ShippedDate,ArtistArtworkId,ArtistId,ConsumerId")] ArtworkOrder artworkOrder)
        {
            if (id != artworkOrder.ArtworkOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artworkOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtworkOrderExists(artworkOrder.ArtworkOrderId))
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
            ViewData["ArtistId"] = new SelectList(_context.Artist, "ArtistId", "ArtistId", artworkOrder.ArtistId);
            ViewData["ArtistArtworkId"] = new SelectList(_context.ArtistArtwork, "ArtistArtworkId", "ArtistArtworkId", artworkOrder.ArtistArtworkId);
            ViewData["ConsumerId"] = new SelectList(_context.Consumer, "ConsumerId", "ConsumerId", artworkOrder.ConsumerId);
            return View(artworkOrder);
        }

        // GET: ArtworkOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artworkOrder = await _context.ArtworkOrder
                .Include(a => a.Artist)
                .Include(a => a.ArtistArtwork)
                .Include(a => a.Consumer)
                .FirstOrDefaultAsync(m => m.ArtworkOrderId == id);
            if (artworkOrder == null)
            {
                return NotFound();
            }

            return View(artworkOrder);
        }

        // POST: ArtworkOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artworkOrder = await _context.ArtworkOrder.FindAsync(id);
            _context.ArtworkOrder.Remove(artworkOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtworkOrderExists(int id)
        {
            return _context.ArtworkOrder.Any(e => e.ArtworkOrderId == id);
        }
    }
}
