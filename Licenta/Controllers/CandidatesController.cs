using Licenta.Models;
using LicentaBUS.BusinessLayer;
using NoBordersConnectionDb;
using System;
using System.Collections.Generic;
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
      
    }
}