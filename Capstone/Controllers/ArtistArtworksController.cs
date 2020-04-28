﻿using System;
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
    public class ArtistArtworksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtistArtworksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ArtistArtworks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ArtistArtwork.Include(a => a.Artist);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ArtistArtworks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistArtwork = await _context.ArtistArtwork
                .Include(a => a.Artist)
                .FirstOrDefaultAsync(m => m.ArtistArtworkId == id);
            if (artistArtwork == null)
            {
                return NotFound();
            }

            return View(artistArtwork);
        }

        // GET: ArtistArtworks/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artist, "ArtistId", "ArtistId");
            return View();
        }

        // POST: ArtistArtworks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArtistArtworkId,Name,InStock,ImgUrl,ArtworkPrice,ArtistId")] ArtistArtwork artistArtwork)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artistArtwork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "ArtistId", "ArtistId", artistArtwork.ArtistId);
            return View(artistArtwork);
        }

        // GET: ArtistArtworks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistArtwork = await _context.ArtistArtwork.FindAsync(id);
            if (artistArtwork == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "ArtistId", "ArtistId", artistArtwork.ArtistId);
            return View(artistArtwork);
        }

        // POST: ArtistArtworks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArtistArtworkId,Name,InStock,ImgUrl,ArtworkPrice,ArtistId")] ArtistArtwork artistArtwork)
        {
            if (id != artistArtwork.ArtistArtworkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artistArtwork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistArtworkExists(artistArtwork.ArtistArtworkId))
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
            ViewData["ArtistId"] = new SelectList(_context.Artist, "ArtistId", "ArtistId", artistArtwork.ArtistId);
            return View(artistArtwork);
        }

        // GET: ArtistArtworks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistArtwork = await _context.ArtistArtwork
                .Include(a => a.Artist)
                .FirstOrDefaultAsync(m => m.ArtistArtworkId == id);
            if (artistArtwork == null)
            {
                return NotFound();
            }

            return View(artistArtwork);
        }

        // POST: ArtistArtworks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artistArtwork = await _context.ArtistArtwork.FindAsync(id);
            _context.ArtistArtwork.Remove(artistArtwork);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistArtworkExists(int id)
        {
            return _context.ArtistArtwork.Any(e => e.ArtistArtworkId == id);
        }
    }
}
