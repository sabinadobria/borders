using Licenta.DataLayer;
using Licenta.Models;
using LicentaBUS.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licenta.Controllers
{
    public class UserController : Controller
    {
        // GET:User
        public ActionResult UserProfile()
        {
            NoBordersDB db = new NoBordersDB();
            int candidateId = Convert.ToInt32(Session["user_id"]);

            try
            {
                //get contact data details for logged in user
                var candidate = db.CandidateProfiles.Single(cand => cand.Id_candidate == candidateId);

                //get the list of all the experiences for the logged in user
                candidate.CandidateExperience = db.CandidateExperiences.Where(x => x.Id_candidate == candidateId).ToList();
                return View(candidate);
            }
             
            catch
            {
               return RedirectToAction("Login", "Login");
            }
           
          

        }

        [HttpPost]
       // [ActionName("UserProfile")]
        public ActionResult UserProfile(CandidateProfile candidateProfile)
        {
            if (ModelState.IsValid)
            {
                NoBordersDB db = new NoBordersDB();
                db.Entry(candidateProfile).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserProfile");
            }
            return View(candidateProfile);
        }


        [HttpGet]
        public ActionResult Preview(int id)
        {
            return View(Candidate.GetCandidateById(id));
        }

        [HttpGet]
        public ActionResult UserDetails(int id)
        {
            return View(Candidate.GetCandidateById(id));
        }

    }
}