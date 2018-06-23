using Licenta.Models;
using LicentaBUS.BusinessLayer;
using NoBordersConnectionDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licenta.Controllers
{
    public class CandidatesController : Controller
    {
        // GET: Table
       
        public ActionResult CandidatesList()
        {
                 return View(Candidate.GetCandidates());
        }
    }
}