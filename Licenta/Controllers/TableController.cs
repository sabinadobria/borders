using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licenta.Controllers
{
    public class TableController : Controller
    {
        // GET: Table
        public ActionResult TableList()
        {
            return View();
        }
    }
}