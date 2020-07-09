﻿using Kosmisch.Sample.OnPremisesAspnetApp.Data;
using Kosmisch.Sample.OnPremisesAspnetApp.Helpers;
using Kosmisch.Sample.OnPremisesAspnetApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Kosmisch.Sample.OnPremisesAspnetApp.Controllers
{
    public class UsersController : Controller
    {
        private MyContext db;

        public UsersController(MyContext db)
        {
            this.db = db;
        }

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Name,Age,ProfileImageName")] User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            user.Id = Guid.NewGuid();
            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Users/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,Name,Age,ProfileImageName")] User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Users/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SendEmailSample()
        {
            EmailHelper.Send();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SaveUserDataSample()
        {
            var users = db.Users.ToList();
            var json = JsonConvert.SerializeObject(users);
            FileHelper.WriteJson(json);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
