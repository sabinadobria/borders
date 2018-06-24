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
        public ActionResult UserProfile(UserBus userBus, UserModel user)
        {
            CandidateProfileDL candDl = new CandidateProfileDL();

            // int candIDD = candDL.getCandidateId(user.email);
            //int candId = Convert.ToInt32(TempData["ID"]);

            int canddidateId = Convert.ToInt32(TempData["ID"]);
            return View(candDl.getCandidateProfileById(canddidateId));
        }
        [HttpPost]
        public ActionResult UpdateCandidate(int id, CandidateProfile candidateProfile)
        {
            try
            {
                CandidateProfileDL candProfileDL = new CandidateProfileDL();

                if (candProfileDL.updateCandidateProfile(candidateProfile))
                {
                    return RedirectToAction("UserProfile", "User");
                }
            }
            catch
            {
                RedirectToAction("UserProfile", "User");
            }
           return RedirectToAction("UserProfile", "User");
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

       //[HttpPost]
        //public ActionResult Edit(Candidate candidate)
        //{
        //    var dataContext = new PetaPoco.Database("sqlserverce");
          // dataContext.Update("Candidate", "id_candidate", candidate);
        //    return RedirectToAction("TableList","Table");
        //}

        
        //public ActionResult Delete(int id)
        //{

        //    var employee = dataContext.SingleOrDefault<Candidate>("Select * from Employee where employeeId=@0",
        //                                                     id);
        //    return View(employee);
        //}

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection formCollection)
        //{
        //    var dataContext = new PetaPoco.Database("sqlserverce");
        //    dataContext.Delete<Employee>(id);
        //    return RedirectToAction("Index");
        //}
    }
}