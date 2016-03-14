using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STI.Models;

namespace STI.Controllers
{
    public class adminHomeController : Controller
    {
        // GET: adminHome
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult alumnos()
        {
            return View(getAlumnos());
        }
        private List<Alumno> getAlumnos()
        {
            Base db = new Base();
            var alumnos = from a in db.Alumnos select a;            
            return alumnos.ToList();
        }
    }
}