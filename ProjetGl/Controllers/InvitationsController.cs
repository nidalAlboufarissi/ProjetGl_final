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
{[Authorize]
    public class InvitationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Invitations
        public ActionResult Index()
        {
            string id = Session["user"].ToString();
            //la liste des invitation recues par l'utilisateurs connecté
            return View(db.Invitations.Where(i => i.Recepteur.Id == id).Include(i => i.Projet).Include(i => i.Emetteur).ToList());
        }

        // GET: Invitations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation invitation = db.Invitations.Find(id);
            if (invitation == null)
            {
                return HttpNotFound();
            }
            return View(invitation);
        }

        // GET: Invitations/Create
        public ActionResult Create()
        {
            string id = Session["user"].ToString();
            //liste des utilisateurs
            ViewBag.RecepteurId = new SelectList(db.Users.Where(u => u.Id != id), "Id", "Nom");
            //liste des projets crees par l'utilisateur connecté
            ViewBag.ProjetId = new SelectList(db.Projets.Where(p => p.Createur.Id == id), "Id", "Nom");
            return View();
        }

        // POST: Invitations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,ProjetId,RecepteurId")]Invitation invitation)
        {
            if (ModelState.IsValid)
            {
                string id = Session["user"].ToString();
                invitation.EmetteurId =id ;
                invitation.Etat = "En attente";

                db.Invitations.Add(invitation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(invitation);
        }

        // GET: Invitations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation invitation = db.Invitations.Find(id);
            if (invitation == null)
            {
                return HttpNotFound();
            }
            return View(invitation);
        }

        // POST: Invitations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Etat")] Invitation invitation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invitation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invitation);
        }

        // GET: Invitations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation invitation = db.Invitations.Find(id);
            if (invitation == null)
            {
                return HttpNotFound();
            }
            return View(invitation);
        }

        // POST: Invitations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invitation invitation = db.Invitations.Find(id);
            db.Invitations.Remove(invitation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Accepter(int id)
        {
            Invitation invitation = db.Invitations.Find(id);
            invitation.Etat = "Acceptée";
            string id_ = Session["user"].ToString();
            Collaboration collaboration = new Collaboration();
            collaboration.CollaborateurId = id_;
            collaboration.ProjetId = invitation.ProjetId;

            db.Collaborations.Add(collaboration);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Refuser(int id)
        {
            Invitation invitation = db.Invitations.Find(id);
            invitation.Etat = "Refusée";

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