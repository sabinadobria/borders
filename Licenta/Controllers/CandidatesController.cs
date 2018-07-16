using Licenta.Models;
using LicentaBUS.BusinessLayer;
using NoBordersConnectionDb;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Licenta.Controllers
{
    public class CandidatesController : Controller
    {
        //load all candidates list
        NoBordersDB db = new NoBordersDB();
        public ActionResult CandidatesList()
        {

            if (string.IsNullOrEmpty((string)Session["Recruiter_email"]))
            {
                return RedirectToAction("Login", "Login");
            }

            try
            {
                List<CandidateProfile> candidateProfiles = db.CandidateProfiles.Where(x=>x.Interest!= "Not interested").ToList();

                List<CandidateExperience> candidateExperiences = db.CandidateExperiences.ToList();

                List<CandidateStudies> candidateStudies = db.CandidateStudies.ToList();

                List<CandidateTechnologies> candidateTechnologies = db.CandidateTechnologies.ToList();

                List<CandidateLanguages> candidateLanguages = db.CandidateLanguages.ToList();

                foreach (var candidate in candidateProfiles)
                {

                    candidate.CandidateExperience = candidateExperiences.Where(x => x.Id_candidate == candidate.Id_candidate).ToList();
                    candidate.CandidateStudies = candidateStudies.Where(x => x.id_candidate == candidate.Id_candidate).ToList();
                    candidate.CandidateLanguages = candidateLanguages.Where(x => x.id_candidate == candidate.Id_candidate).ToList();
                    candidate.CandidateTechnologies = candidateTechnologies.Where(x => x.id_candidate == candidate.Id_candidate).ToList();
                }
            
                
               
              

                return View(candidateProfiles);
            }
            catch
            {
                return View();
            }
         
        }

        //mark a candidate = saved
        public JsonResult SaveProfile(int? Id_candidate)
        {
            bool result = false;

            if (Id_candidate != null)
            {
                ModelState.Clear();
                NoBordersDB db = new NoBordersDB();


                var candidate = db.CandidateProfiles.Single(cand => cand.Id_candidate == Id_candidate);

                //get the list of all the experiences for the logged in user
                candidate.CandidateExperience = db.CandidateExperiences.Where(x => x.Id_candidate == Id_candidate).ToList();
                //get the list of all the studies for the logged user
                candidate.CandidateStudies = db.CandidateStudies.Where(x => x.id_candidate == Id_candidate).ToList();
                //get the list of all the technologies for the logged user
                candidate.CandidateTechnologies = db.CandidateTechnologies.Where(x => x.id_candidate == Id_candidate).ToList();
                //get the list of all the languages for the logged user
                candidate.CandidateLanguages = db.CandidateLanguages.Where(x => x.id_candidate == Id_candidate).ToList();

                SavedCandidate savedCandidate = new SavedCandidate();

                //retrieve the email of the logged in recruiter
                string recruiterEmail = (string)Session["recruiter_email"];
                var checkemail = db.SavedCandidates.FirstOrDefault(x => x.Email == candidate.Email && x.Recruiter_email == recruiterEmail);

                //check if the candidate is allready saved
                if (checkemail == null)
                {
                    savedCandidate.Email = candidate.Email;
                    savedCandidate.First_name = candidate.First_name;
                    savedCandidate.Last_name = candidate.Last_name;
                    savedCandidate.Country_to_work = candidate.Country_To_Work;
                    savedCandidate.Status = candidate.Interest;
                    savedCandidate.Recruiter_email = Session["recruiter_email"].ToString();

                    savedCandidate.Process = "";

                    List<CandidateTechnologies> skills = candidate.CandidateTechnologies.Where(x => x.tech_level == "Senior    ").ToList();
                    if (skills.Count == 0)
                    {
                        skills = candidate.CandidateTechnologies.Where(x => x.tech_level == "Middle    ").ToList();

                        if (skills.Count == 0)
                        {
                            skills = candidate.CandidateTechnologies.Where(x => x.tech_level == "Junior    ").ToList();
                            if (skills.Count == 0)
                            {
                                savedCandidate.Skill = "N/A";
                                savedCandidate.Experience = "N/A";
                            }
                            else
                            {
                                savedCandidate.Skill = skills[0].tech_name;
                                savedCandidate.Experience = skills[0].tech_level;
                            }
                        }
                        else
                        {
                            savedCandidate.Skill = skills[0].tech_name;
                            savedCandidate.Experience = skills[0].tech_level;
                        }



                    }
                    else
                    {
                        savedCandidate.Skill = skills[0].tech_name;
                        savedCandidate.Experience = skills[0].tech_level;
                    }


                    db.SavedCandidates.Add(savedCandidate);
                    db.SaveChanges();
                    result = true;
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
             return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        //load saved candidates list
        public ActionResult SavedCandidates()
        {
            NoBordersDB db = new NoBordersDB();
            List<SavedCandidate> savedCandidates;
            string recruiter = "";

            if (string.IsNullOrEmpty((string)Session["Recruiter_email"]))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                recruiter = Session["Recruiter_email"].ToString();
                savedCandidates = db.SavedCandidates.Where(x => x.Recruiter_email == recruiter).ToList();
                return View(savedCandidates);
            }
             
        }
     
        //preview a candidate profile
        public ActionResult Preview(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateProfile candidateProfile = db.CandidateProfiles.Find(id);
            //get contact data details for logged in user

            //get the list of all the experiences for the logged in user
            candidateProfile.CandidateExperience = db.CandidateExperiences.Where(x => x.Id_candidate == id).ToList();
            //get the list of all the studies for the logged user
            candidateProfile.CandidateStudies = db.CandidateStudies.Where(x => x.id_candidate == id).ToList();
            //get the list of all the technologies for the logged user
            candidateProfile.CandidateTechnologies = db.CandidateTechnologies.Where(x => x.id_candidate == id).ToList();
            //get the list of all the languages for the logged user
            candidateProfile.CandidateLanguages = db.CandidateLanguages.Where(x => x.id_candidate == id).ToList();

            if (candidateProfile == null)
            {
                return HttpNotFound();
            }
            return View(candidateProfile);
        }

        //delete  saved candidate 
        public JsonResult DeleteSavedCandidate (int? Id_SavedCandidate)
        {
            SavedCandidate savedCandidate = db.SavedCandidates.Find(Id_SavedCandidate);

            bool result = false;

            if (savedCandidate != null)
            {
                db.SavedCandidates.Remove(savedCandidate);
                db.SaveChanges();
                result = true;
            }
          
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        //set process for saved candidate
        public JsonResult SetProcess(int? Id_SavedCandidate,string Process)
        {
            SavedCandidate savedCandidate = db.SavedCandidates.Find(Id_SavedCandidate);
            bool result = false;

            if (savedCandidate != null)
            {
                savedCandidate.Process = Process;
                db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}