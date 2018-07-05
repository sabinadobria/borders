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
        // GET: Table
        NoBordersDB db = new NoBordersDB();
        public ActionResult CandidatesList()
        {
            try
            {
                List<CandidateProfile> candidateProfiles = db.CandidateProfiles.ToList();

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
            
                
                //return View(result);
                //if (result.tech_level)
              

                return View(candidateProfiles);
            }
            catch
            {
                return View();
            }
         
        }


        public ActionResult SaveProfile(int? id)
        {
            if (id != null)
            {
                ModelState.Clear();
                NoBordersDB db = new NoBordersDB();


                var candidate = db.CandidateProfiles.Single(cand => cand.Id_candidate == id);

                //get the list of all the experiences for the logged in user
                candidate.CandidateExperience = db.CandidateExperiences.Where(x => x.Id_candidate == id).ToList();
                //get the list of all the studies for the logged user
                candidate.CandidateStudies = db.CandidateStudies.Where(x => x.id_candidate == id).ToList();
                //get the list of all the technologies for the logged user
                candidate.CandidateTechnologies = db.CandidateTechnologies.Where(x => x.id_candidate == id).ToList();
                //get the list of all the languages for the logged user
                candidate.CandidateLanguages = db.CandidateLanguages.Where(x => x.id_candidate == id).ToList();

                SavedCandidate savedCandidate = new SavedCandidate();

                var checkemail = db.SavedCandidates.FirstOrDefault(x => x.Email == candidate.Email);

                //check if the candidate is allready saved
                if (checkemail == null)
                {
                    savedCandidate.Email = candidate.Email;
                    savedCandidate.First_name = candidate.First_name;
                    savedCandidate.Last_name = candidate.Last_name;
                    savedCandidate.Country_to_work = candidate.Country_To_Work;
                    savedCandidate.Status = candidate.Interest;

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
                    return RedirectToAction("SavedCandidates", "Candidates");
                }
                return RedirectToAction("DoSomething");

            }
             
            return View("CandidatesList");

        }


        public ActionResult DoSomething()
        {
            return Content("<script language='javascript' type='text/javascript'>alert('Candidate allready saved');</script>");
        }

        //GET :Favorites List
        public ActionResult SavedCandidates()
        {
            NoBordersDB db = new NoBordersDB();

            List<SavedCandidate> savedCandidates = db.SavedCandidates.ToList();
            return View(savedCandidates);
        }

        // GET: CandidateProfiles/Edit/5
        public ActionResult SetCandidateProces(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavedCandidate savedCandidate = db.SavedCandidates.Find(id);
            if (savedCandidate == null)
            {
                return HttpNotFound();
            }
            return View(savedCandidate);
        }

        // POST: CandidateProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetCandidateProces(SavedCandidate savedCandidate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(savedCandidate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SavedCandidates");
            }
            return View(savedCandidate);
        }


        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult SavedCandidates(List<SavedCandidate> SavedCandidates)
        //{
        //    NoBordersDB db = new NoBordersDB();
        //    ModelState.Clear();
        //    if (ModelState.IsValid)
        //    {
        //        //update candidate contact data
        //       // db.Entry(savedCandidate).State = System.Data.Entity.EntityState.Modified;
                
        //        try
        //        {
        //            db.SaveChanges();
        //        }
        //        catch (DbUpdateException ex)
        //        {
        //            string error = ex.Message;
        //        }

        //        return RedirectToAction("SavedCandidates");
        //    }
        //    return View();
        //}

        public ActionResult Preview(int? id)
        {
            if(id == null)
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

        //clasic delete for saved candidate

        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SavedCandidate savedCandidate = db.SavedCandidates.Find(id);
        //    if (savedCandidate == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    db.SavedCandidates.Remove(savedCandidate);
        //    db.SaveChanges();
        //    return RedirectToAction("SavedCandidates", "Candidates");
        //}

       //modal delete for saved candidate 
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