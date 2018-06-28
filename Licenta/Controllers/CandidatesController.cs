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
            
            return View(db.CandidateProfiles.ToList());
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