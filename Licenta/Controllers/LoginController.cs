using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licenta.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult LogIn()
        {

            return View();
        }

        public ActionResult RegisterCandidate()
        {
            return View();
        }

        public ActionResult RegisterCompany()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}