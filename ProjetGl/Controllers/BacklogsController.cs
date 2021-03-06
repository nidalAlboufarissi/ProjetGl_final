﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjetGl.Models;

namespace ProjetGl.Controllers
{
    [Authorize]
    public class BacklogsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Backlogs
        public ActionResult Index()
        {
            string email = Session["user"].ToString();
            ApplicationUser user = db.Users.Where(u => u.Email == email).First();
            return View(db.Backlogs.Where(P=>P.Projet.id_client==user.Id).ToList());
        }

        // GET: Backlogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Backlog backlog = db.Backlogs.Find(id);
            if (backlog == null)
            {
                return HttpNotFound();
            }
            return View(backlog);
        }

        // GET: Backlogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Backlogs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,id_Project")] Backlog backlog)
        {
            if (ModelState.IsValid)
            {
                db.Backlogs.Add(backlog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(backlog);
        }

        // GET: Backlogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Backlog backlog = db.Backlogs.Find(id);
            if (backlog == null)
            {
                return HttpNotFound();
            }
            return View(backlog);
        }

        // POST: Backlogs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,id_Project")] Backlog backlog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(backlog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(backlog);
        }

        // GET: Backlogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Backlog backlog = db.Backlogs.Find(id);
            if (backlog == null)
            {
                return HttpNotFound();
            }
            return View(backlog);
        }

        // POST: Backlogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Backlog backlog = db.Backlogs.Find(id);
            db.Backlogs.Remove(backlog);
            db.SaveChanges();
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
