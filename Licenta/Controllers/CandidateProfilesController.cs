using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Licenta.Models;

namespace Licenta.Controllers
{
    public class CandidateProfilesController : Controller
    {
        private NoBordersDB db = new NoBordersDB();

        // GET: CandidateProfiles
        public ActionResult Index()
        {
            return View(db.CandidateProfiles.ToList());
        }

        // GET: CandidateProfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateProfile candidateProfile = db.CandidateProfiles.Find(id);

            if (candidateProfile == null)
            {
                return HttpNotFound();
            }
            return View(candidateProfile);
        }

        // GET: CandidateProfiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CandidateProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_candidate,Last_name,First_name,Email,Phone,Address,City,Country,AboutMe")] CandidateProfile candidateProfile)
        {
            if (ModelState.IsValid)
            {
                db.CandidateProfiles.Add(candidateProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(candidateProfile);
        }

        // GET: CandidateProfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateProfile candidateProfile = db.CandidateProfiles.Find(id);
            if (candidateProfile == null)
            {
                return HttpNotFound();
            }
            return View(candidateProfile);
        }

        // POST: CandidateProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_candidate,Last_name,First_name,Email,Phone,Address,City,Country,AboutMe")] CandidateProfile candidateProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidateProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(candidateProfile);
        }

        // GET: CandidateProfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateProfile candidateProfile = db.CandidateProfiles.Find(id);
            if (candidateProfile == null)
            {
                return HttpNotFound();
            }
            return View(candidateProfile);
        }

        // POST: CandidateProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CandidateProfile candidateProfile = db.CandidateProfiles.Find(id);
            db.CandidateProfiles.Remove(candidateProfile);
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
