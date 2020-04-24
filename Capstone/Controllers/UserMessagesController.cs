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

namespace Capstone.Controllers
{
    public class UserMessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserMessagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserMessages
        public ActionResult GetMessages()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var foundUser = _context.Users.Where(s => s.Id == userId).SingleOrDefault();
            var userMessages = _context.UserMessages.Where(a => a.RecipientUserId == foundUser.Id).Select(a=>a);
            ViewBag.UserList = _context.UserMessages.Where(a => a.IdentityUser.Id == a.IdentityUserId).Distinct();
            return View(userMessages);
        }

        public IActionResult SendMessage(string id)
        {
            var loggedInUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Sender = _context.Users.Where(a => a.Id == loggedInUserId).SingleOrDefault();
            ViewBag.Recipient = _context.Users.Where(a => a.Id == id).SingleOrDefault();
            return View();
        }

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Consumers");
        }

        // GET: UserMessages/Details/5
        public ActionResult Details(int? id)
        {
            return View();
        }

        // POST: UserMessage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserMessage userMessage)
        {
            try
            {
                // TODO: Add insert logic here
                userMessage = new UserMessage();
                userMessage.DateTime = DateTime.UtcNow;
                _context.UserMessages.Add(userMessage);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserMessages/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }


        // POST: UserMessage/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
