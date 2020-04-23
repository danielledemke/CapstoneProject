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
    public class ConsumerRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsumerRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ConsumerRequests
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Consumer");
        }

        // GET: ConsumerRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumerRequest = await _context.ConsumerRequest
                .Include(c => c.Consumer)
                .FirstOrDefaultAsync(m => m.ConsumerRequestId == id);
            if (consumerRequest == null)
            {
                return NotFound();
            }

            return View(consumerRequest);
        }

        // GET: ConsumerRequests/Create
        public IActionResult Create()
        {
            ConsumerRequest consumerRequest = new ConsumerRequest();
            //ViewData["ConsumerId"] = new SelectList(_context.Consumer, "ConsumerId", "ConsumerId");
            return View(consumerRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ConsumerRequest consumerRequest)
        {
            try
            {
                // TODO: Add insert logic here
               consumerRequest.DateTime = DateTime.UtcNow;
                _context.ConsumerRequest.Add(consumerRequest);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ConsumerRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            return View();
        }

        // POST: ConsumerRequests/Edit/5
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("ConsumerRequestId,DateTime,Request,ConsumerId")] ConsumerRequest consumerRequest)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: ConsumerRequests/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            { 
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

       
    }
}
