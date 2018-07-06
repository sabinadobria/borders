﻿using Licenta.DataLayer;
using Licenta.Models;
using LicentaBUS.BusinessLayer;
using NoBordersConnectionDb;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                if (userBus.IsValid(user.email, user.password,user.userTypeId))
                   {
                    CandidateProfileDL candDl = new CandidateProfileDL();
                    NoBordersDB db = new NoBordersDB();
                    
                    //checking the user type ( candidate/recruiter)
                    var ut = userBus.userTypeId(user.email, user.password);
                    if (ut == 1)
                    {
                        //storing logged -in user id in session variable
                        var id = candDl.getCandidateId(user.email);
                        Session["candidate_id"] = id;


                        //getting logged in user details
                        var dummyModel = db.CandidateProfiles.Single(cand => cand.Id_candidate == id);

                        //getting the full name of the logged in user to be displayed on the UserProfile page
                        Session["user_name"] = dummyModel.First_name + " " + dummyModel.Last_name;

                        Membership.ValidateUser(user.email, user.password);
                        return RedirectToAction("UserProfile", "User");

                    }
                    else if (ut == 2)
                    {
                        Session["recruiter_email"] = user.email;
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
            //Setting all the session variables = null 

            Session.Abandon();
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

            NoBordersDB db = new NoBordersDB();
            var checkmail = db.Users.SingleOrDefault(x => x.email == candidate.email);
            //check if email is allready created
            if (checkmail == null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        //calling stored procedure
                        CandidateRegisterDL candDL = new CandidateRegisterDL();
                        candDL.registerCandidate(candidate);

                        var user = db.CandidateProfiles.Single(x => x.Email == candidate.email);
                        Session["candidate_id"] = user.Id_candidate;
                        Session["user_name"] = user.First_name + " " + user.Last_name;
                        return RedirectToAction("userProfile", "user");

                    }
                    return View();
                }
                catch
                {
                    return View();
                }
            }
            return View();
             
        }
        [HttpGet]
        public ActionResult RegisterCompany()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterCompany(CompanyRegister company)
        {
            NoBordersDB db = new NoBordersDB();
            var checkmail = db.Users.SingleOrDefault(x => x.email == company.email);
            //check if email is allready created
            if (checkmail == null)
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

                }
                catch
                {
                    return View();

                }
            }
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}