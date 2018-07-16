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
        string oldEmail = null;
        NoBordersDB db = new NoBordersDB();
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
                //for (int i = 0; i < candidateProfile.CandidateExperience.Count(); i++)
                //{
                //    db.Entry(candidateProfile.CandidateExperience[i]).State = System.Data.Entity.EntityState.Modified;
                //}
                ////loop through every candidate studies and update in database
                //for (int i = 0; i < candidateProfile.CandidateStudies.Count(); i++)
                //{
                //    db.Entry(candidateProfile.CandidateStudies[i]).State = System.Data.Entity.EntityState.Modified;
                //}
                ////loop through every candidate tech and update in database
                //for (int i = 0; i < candidateProfile.CandidateTechnologies.Count(); i++)
                //{
                //    db.Entry(candidateProfile.CandidateTechnologies[i]).State = System.Data.Entity.EntityState.Modified;
                //}
                ////loop through every candidate languages and update in database
                //for (int i = 0; i < candidateProfile.CandidateLanguages.Count(); i++)
                //{
                //    db.Entry(candidateProfile.CandidateLanguages[i]).State = System.Data.Entity.EntityState.Modified;
                //}
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

        //preview candidate profile
        [HttpGet]
        public ActionResult Preview(int id)
        {
            return View(Candidate.GetCandidateById(id));
        }



        //add language
        public JsonResult AddLanguage(int? Id_candidate, string language, string language_level)
        {
            CandidateLanguages candidateLanguages = new CandidateLanguages();
            bool result = false;

            candidateLanguages.id_candidate = (int)Id_candidate;
            candidateLanguages.language = language;
            candidateLanguages.language_level = language_level;
            
            db.CandidateLanguages.Add(candidateLanguages);
            db.SaveChanges();
            result = true;
        

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //add Tech 
        public JsonResult AddTech(int? Id_candidate, string tech_name,string tech_level)
        {
            CandidateTechnologies candidateTechnologies = new CandidateTechnologies();
            bool result = false;

            candidateTechnologies.id_candidate = (int)Id_candidate;
            candidateTechnologies.tech_name = tech_name;
            candidateTechnologies.tech_level = tech_level;

            db.CandidateTechnologies.Add(candidateTechnologies);
            db.SaveChanges();
            result = true;
       

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //add Studies 
        public JsonResult AddStudies(int? Id_candidate, string school, string diploma,string from_date, string to_date,string section)
        {
            CandidateStudies candidateStudies = new CandidateStudies();
            bool result = false;

            candidateStudies.id_candidate = (int)Id_candidate;
            candidateStudies.school = school;
            candidateStudies.diploma = diploma;
            candidateStudies.from_date = from_date;
            candidateStudies.to_date = to_date;
            candidateStudies.section = section;


            db.CandidateStudies.Add(candidateStudies);
            db.SaveChanges();
            result = true;
           

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //add Experience 
        public JsonResult AddExperience(int? Id_candidate, string companyname,string from_date,string to_date,string description,string position)
        {
            CandidateExperience candidateExperience = new CandidateExperience();
            bool result = false;

            candidateExperience.Id_candidate = (int)Id_candidate;
            candidateExperience.company_name = companyname;
            candidateExperience.from_date = from_date;
            candidateExperience.to_date = to_date;
            candidateExperience.description = description;
            candidateExperience.position = position;

            db.CandidateExperiences.Add(candidateExperience);
            db.SaveChanges();

            result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }




        //remove language row
        public JsonResult DeleteLanguage(int? Id_candidate, int? id_language)
        {
            NoBordersDB db = new NoBordersDB();

            CandidateProfile candidateProfile = db.CandidateProfiles.Find(Id_candidate);
            CandidateLanguages candidateLanguage = db.CandidateLanguages.SingleOrDefault(x => x.id_candidate == Id_candidate && x.id_language==id_language);

            bool result = false;

            if (candidateLanguage != null)
            {
                db.CandidateLanguages.Remove(candidateLanguage);
                db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //remove tech row
        public JsonResult DeleteTech(int? Id_candidate, int? id_tech)
        {
            NoBordersDB db = new NoBordersDB();

            CandidateProfile candidateProfile = db.CandidateProfiles.Find(Id_candidate);
            CandidateTechnologies candidateTechnology = db.CandidateTechnologies.SingleOrDefault(x => x.id_candidate == Id_candidate && x.id_tech == id_tech);

            bool result = false;

            if (candidateTechnology != null)
            {
                db.CandidateTechnologies.Remove(candidateTechnology);
                db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //remove studies row
        public JsonResult DeleteStudies(int? Id_candidate, int? id_education)
        {
            NoBordersDB db = new NoBordersDB();

            CandidateProfile candidateProfile = db.CandidateProfiles.Find(Id_candidate);
            CandidateStudies candidateStudies = db.CandidateStudies.SingleOrDefault(x => x.id_candidate == Id_candidate && x.id_education == id_education);

            bool result = false;

            if (candidateStudies != null)
            {
                db.CandidateStudies.Remove(candidateStudies);
                db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //remove experience row
        public JsonResult DeleteExperience(int? Id_candidate, int? Id_experience)
        {
            NoBordersDB db = new NoBordersDB();

            CandidateProfile candidateProfile = db.CandidateProfiles.Find(Id_candidate);
            CandidateExperience candidateExperience = db.CandidateExperiences.SingleOrDefault(x => x.Id_candidate == Id_candidate && x.Id_experience == Id_experience);

            bool result = false;

            if (candidateExperience != null)
            {
                db.CandidateExperiences.Remove(candidateExperience);
                db.SaveChanges();
                result = true;
            }   

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //public ActionResult EditTech(int? Id_candidate, int id_tech)
        //{
        //    CandidateTechnologies toUpdateTech = db.CandidateTechnologies.SingleOrDefault(x => x.id_candidate == Id_candidate && x.id_tech == id_tech);

        //    return PartialView("_EditTech",toUpdateTech);
        //}


        //Edit  tech 
        public JsonResult EditTech(int? Id_candidate, int id_tech, string tech_level, string tech_name)
        {
            
            CandidateTechnologies techToUpdate = db.CandidateTechnologies.Single(x => x.id_candidate == Id_candidate && x.id_tech == id_tech);
            bool result = false;

            techToUpdate.tech_name = tech_name;
            techToUpdate.tech_level = tech_level;
            
             db.SaveChanges();

            result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //Edit  language 
        public JsonResult EditLanguage(int? Id_candidate,int id_language, string language, string language_level)
        {

            CandidateLanguages languageToUpdate = db.CandidateLanguages.Single(x => x.id_candidate == Id_candidate && x.id_language == id_language);
            bool result = false;

            languageToUpdate.language = language;
            languageToUpdate.language_level = language_level;

            db.SaveChanges();

            result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //Edit  studies 
        public JsonResult EditStudies(int? Id_candidate, int id_education,string school, string diploma, string from_date, string to_date, string section)
        {

            CandidateStudies studiesToUpdate = db.CandidateStudies.Single(x => x.id_candidate == Id_candidate && x.id_education == id_education);
            bool result = false;

            studiesToUpdate.school = school;
            studiesToUpdate.diploma = diploma;
            studiesToUpdate.from_date = from_date;
            studiesToUpdate.to_date = to_date;
            studiesToUpdate.section = section;

            db.SaveChanges();

            result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //Edit  experience 
        public JsonResult EditExperience(int? Id_candidate, int id_experience,string companyname, string from_date, string to_date, string description, string position)
        {

            CandidateExperience experienceToUpdate = db.CandidateExperiences.Single(x => x.Id_candidate == Id_candidate && x.Id_experience == id_experience);
            bool result = false;

            experienceToUpdate.company_name = companyname;
            experienceToUpdate.from_date = from_date;
            experienceToUpdate.to_date = to_date;
            experienceToUpdate.description = description;
            experienceToUpdate.position = position;

            db.SaveChanges();

            result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}