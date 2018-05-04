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
        // GET: User
        public ActionResult UserProfile()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Preview(int id)
        {
            return View(Candidate.GetCandidateById(id));
        }

        public ActionResult UserDetails(int id)
        {
            return View(Candidate.GetCandidateById(id));
        }

        [HttpPost]
        public ActionResult Edit(Candidate candidate)
        {
            var dataContext = new PetaPoco.Database("sqlserverce");
            dataContext.Update("Candidate", "id_candidate", candidate);
            return RedirectToAction("TableList","Table");
        }

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