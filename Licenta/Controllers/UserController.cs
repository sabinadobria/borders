using Licenta.DataLayer;
using Licenta.Models;
using LicentaBUS.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licenta.Controllers
{
    public class UserController : Controller
    {
        // GET:UserDetails
        public ActionResult UserProfile()
        {
            NoBordersDB db = new NoBordersDB();
            int candidateId = Convert.ToInt32(Session["candidate_id"]);

            try
            {
                //get contact data details for logged in user
                var candidate = db.CandidateProfiles.Single(cand => cand.Id_candidate == candidateId);

                //get the list of all the experiences for the logged in user
                candidate.CandidateExperience = db.CandidateExperiences.Where(x => x.Id_candidate == candidateId).ToList();
                //get the list of all the studies for the logged user
                candidate.CandidateStudies = db.CandidateStudies.Where(x => x.id_candidate == candidateId).ToList();
                //get the list of all the technologies for the logged user
                candidate.CandidateTechnologies = db.CandidateTechnologies.Where(x => x.id_candidate == candidateId).ToList();
                //get the list of all the languages for the logged user
                candidate.CandidateLanguages = db.CandidateLanguages.Where(x => x.id_candidate == candidateId).ToList();

                
                return View(candidate);
            }
             
            catch (Exception ex)
            {
               string error = ex.Message;
               return RedirectToAction("Login", "Login");
            }

        }

        //update candidate informations 

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UserProfile(CandidateProfile candidateProfile)
        {
            NoBordersDB db = new NoBordersDB();
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                //update candidate contact data
                db.Entry(candidateProfile).State = System.Data.Entity.EntityState.Modified;
                Session["user_name"] = candidateProfile.First_name + " " + candidateProfile.Last_name;


                //loop through every candidate experience and update in database
                for (int i = 0; i < candidateProfile.CandidateExperience.Count(); i++)
                {
                    db.Entry(candidateProfile.CandidateExperience[i]).State = System.Data.Entity.EntityState.Modified;
                }
                //loop through every candidate studies and update in database
                for (int i = 0; i < candidateProfile.CandidateStudies.Count(); i++)
                {
                    db.Entry(candidateProfile.CandidateStudies[i]).State = System.Data.Entity.EntityState.Modified;
                }
                //loop through every candidate tech and update in database
                for (int i = 0; i < candidateProfile.CandidateTechnologies.Count(); i++)
                {
                    db.Entry(candidateProfile.CandidateTechnologies[i]).State = System.Data.Entity.EntityState.Modified;
                }
                //loop through every candidate languages and update in database
                for (int i = 0; i < candidateProfile.CandidateLanguages.Count(); i++)
                {
                    db.Entry(candidateProfile.CandidateLanguages[i]).State = System.Data.Entity.EntityState.Modified;
                }
                //perform the update in all the tables 
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                   string error =  ex.Message;
                }

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