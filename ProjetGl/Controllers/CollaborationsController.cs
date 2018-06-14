using System;
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
    public class CollaborationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Collaborations
        public ActionResult Index()
        {
            var collaborations = db.Collaborations.Include(c => c.Collaborateur).Include(c => c.Projet);
            return View(collaborations.ToList());
        }

        // GET: Collaborations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collaboration collaboration = db.Collaborations.Find(id);
            if (collaboration == null)
            {
                return HttpNotFound();
            }
            return View(collaboration);
        }

        // GET: Collaborations/Create
        public ActionResult Create()
        {
            ViewBag.CollaborateurId = new SelectList(db.Users, "Id", "Nom");
            ViewBag.ProjetId = new SelectList(db.Projets, "Id", "Nom");
            return View();
        }

        // POST: Collaborations/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProjetId,CollaborateurId")] Collaboration collaboration)
        {
            if (ModelState.IsValid)
            {
                db.Collaborations.Add(collaboration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CollaborateurId = new SelectList(db.Users, "Id", "Nom", collaboration.CollaborateurId);
            ViewBag.ProjetId = new SelectList(db.Projets, "Id", "Nom", collaboration.ProjetId);
            return View(collaboration);
        }

        // GET: Collaborations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collaboration collaboration = db.Collaborations.Find(id);
            if (collaboration == null)
            {
                return HttpNotFound();
            }
            ViewBag.CollaborateurId = new SelectList(db.Users, "Id", "Nom", collaboration.CollaborateurId);
            ViewBag.ProjetId = new SelectList(db.Projets, "Id", "Nom", collaboration.ProjetId);
            return View(collaboration);
        }

        // POST: Collaborations/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjetId,CollaborateurId")] Collaboration collaboration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(collaboration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CollaborateurId = new SelectList(db.Users, "Id", "Nom", collaboration.CollaborateurId);
            ViewBag.ProjetId = new SelectList(db.Projets, "Id", "Nom", collaboration.ProjetId);
            return View(collaboration);
        }

        // GET: Collaborations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collaboration collaboration = db.Collaborations.Find(id);
            if (collaboration == null)
            {
                return HttpNotFound();
            }
            return View(collaboration);
        }

        // POST: Collaborations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Collaboration collaboration = db.Collaborations.Find(id);
            db.Collaborations.Remove(collaboration);
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
