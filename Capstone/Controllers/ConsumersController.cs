﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Capstone.Data;
using Capstone.Models;
using Capstone.Contracts;
using Capstone.Services;
using System.Security.Claims;

namespace Capstone.Controllers
{
    public class ConsumersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILocationService _locationService;

        public ConsumersController(ApplicationDbContext context, ILocationService locationService)
        {
            _context = context;
            _locationService = locationService;

        }

        // GET: Consumers
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var consumer = _context.Consumer.Where(a=>a.IdentityUserId == userId).SingleOrDefault();
            return View(consumer);
        }

        // GET: Consumers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumer = await _context.Consumer
                .Include(c => c.IdentityUser)
                .FirstOrDefaultAsync(m => m.ConsumerId == id);
            if (consumer == null)
            {
                return NotFound();
            }

            return View(consumer);
        }

        // GET: Consumers/Create
        public IActionResult Create()
        {
            //ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            Consumer consumer = new Consumer();
            return View(consumer);
        }

        // POST: Consumers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsumerId,FirstName,LastName,StreetAddress,ZipCode,Longitude,Latitude,IdentityUserId")] Consumer consumer)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                consumer.IdentityUserId = userId;
                var coords = await _locationService.GetUserCoords(consumer);
                consumer.Latitude = coords.results[0].geometry.location.lat;
                consumer.Longitude = coords.results[0].geometry.location.lng;
                _context.Add(consumer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", consumer.IdentityUserId);
            return View(consumer);
        }

        // GET: Consumers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumer = await _context.Consumer.FindAsync(id);
            if (consumer == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", consumer.IdentityUserId);
            return View(consumer);
        }

        // POST: Consumers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConsumerId,FirstName,LastName,StreetAddress,ZipCode,Longitude,Latitude,IdentityUserId")] Consumer consumer)
        {
            if (id != consumer.ConsumerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consumer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsumerExists(consumer.ConsumerId))
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
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", consumer.IdentityUserId);
            return View(consumer);
        }

        // GET: Consumers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumer = await _context.Consumer
                .Include(c => c.IdentityUser)
                .FirstOrDefaultAsync(m => m.ConsumerId == id);
            if (consumer == null)
            {
                return NotFound();
            }

            return View(consumer);
        }

        // POST: Consumers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consumer = await _context.Consumer.FindAsync(id);
            _context.Consumer.Remove(consumer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult SearchArtists()
        {
            
            var artists = _context.Artist.Distinct().ToList();
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

            return View(artists);
        }

        private bool ConsumerExists(int id)
        {
            return _context.Consumer.Any(e => e.ConsumerId == id);
        }
    }
}
