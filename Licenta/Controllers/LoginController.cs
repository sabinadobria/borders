using Licenta.DataLayer;
using Licenta.Models;
using LicentaBUS.BusinessLayer;
using NoBordersConnectionDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Licenta.Controllers
{
    public class LoginController : Controller
    {

        [HttpGet]
        public ActionResult LogIn()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(UserBus userBus,UserModel user)
        {

            if (ModelState.IsValid)
            {
                if (userBus.IsValid(user.email, user.password,user.userType))
                {
                    //FormsAuthentication.SetAuthCookie(user.email, );
                    var ut = userBus.userTypeId(user.email, user.password);
                    if (ut == 1)
                    {
                        return RedirectToAction("UserProfile", "User");
                    }
                    else if (ut == 2)
                    {
                        return RedirectToAction("CandidatesList", "Candidates");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(user);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Default");
        }

        [HttpGet]
        public ActionResult RegisterCandidate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterCandidate(CandidateRegister candidate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CandidateRegisterDL candDL = new CandidateRegisterDL();
                    candDL.registerCandidate(candidate);
                    return RedirectToAction("userProfile", "user");

                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult RegisterCompany()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterCompany(CompanyRegister company)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RecruiterRegisterDL recruiterDL = new RecruiterRegisterDL();
                    recruiterDL.registerRecruiter(company);
                    return RedirectToAction("CandidatesList", "Candidates");
                }
                return View();

            } catch
            {
                return View();

            }
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}