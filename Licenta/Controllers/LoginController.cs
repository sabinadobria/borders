using Licenta.DataLayer;
using Licenta.Models;
using LicentaBUS.BusinessLayer;
using NoBordersConnectionDb;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
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
        public ActionResult Login(UserBus userBus, UserModel user)
        {
            if (ModelState.IsValid)
            {
                if (userBus.IsValid(user.email, user.password, user.userTypeId))
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
                        var dummyModel = db.CandidateProfiles.SingleOrDefault(cand => cand.Id_candidate == id);

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

        [NonAction]
        public void SendVerificationLinkEmail(string email, string resetCode)
        {
            var verifyUrl = "/Login/ResetPassword/" + resetCode;
            string mailSubject = "";
            string mailBody = "";
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("contactnoborders@gmail.com", "NoBorders");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "wurqoasfwolniiim";

            mailSubject = "Reset password";
            mailBody = "Hi, <br/> <br/> We got a request for reset your account password. Please click on the below link to reset your password." +
                "<br/><br/><a href=" + link + "> Reset password link </a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = mailSubject,
                Body = mailBody,
                IsBodyHtml = true
            })

                smtp.Send(message);

        }

        public ActionResult ForgotPassword()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {

            //check if the email is valid 
            //generate reset password link
            //sent email 

            string message = "";

            using (NoBordersDB db = new NoBordersDB())
            {
                var account = db.Users.Where(x => x.email == email).FirstOrDefault();
                if (account != null)
                {
                    //send email for reset password 
                    string resetCode = Guid.NewGuid().ToString();

                    SendVerificationLinkEmail(account.email, resetCode);
                    account.reset_password_code = resetCode;

                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    message = "Reset password link has been sent to your email.";
                }
                else
                {
                    message = "This is not a register email, please check again.";
                }
            }

            ViewBag.Message = message;
            return View();
        }

        public ActionResult ResetPassword(string id)
        {
            //verify the reset password link
            //find account associated with the link
            //redirect to reset password page


            using (NoBordersDB db = new NoBordersDB())
            {
                var user = db.Users.Where(x => x.reset_password_code == id).FirstOrDefault();

                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {

            var message = "";
            if (ModelState.IsValid)
            {
                using (NoBordersDB db = new NoBordersDB())
                {
                    var user = db.Users.Where(a => a.reset_password_code == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        user.password = /*Crypto.Hash(*/model.NewPassword;
                        user.reset_password_code = "";
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();
                        message = "New password updated successfully";
                    }
                }
            }
            else
            {
                message = "Something invalid";
                return View();
            }
            ViewBag.Message = message;
            return View(model);
        }
    }
}