using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STI.Models;

namespace STI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult salir()
        {
            Session.RemoveAll();
            Session["loged"] = false;
            return View("Index");
        }
        /*public ActionResult datos()
        {
            //Base db = new Base();            
            
           // return View(a);
        }*/
        public JsonResult login(Clave c)
        {
            Base db = new Base();
            Alumno a = new Alumno();
            Profesor p = new Profesor();
            var admin = from u in db.Profesores from cl in db.Claves where u.Nombre == c.login && cl.login == c.login && cl.password == c.password select u;            
            if (admin.Count() > 0)
            {
                p = admin.First();
                Session["loged"] = true;
                Session["nombre"] = p.Nombre;
                Session["userID"] = p.ProfesorId;
                Session["admin"] = true;
                return Json("admin");                
            }
            else
            {
                var user = from u in db.Alumnos where u.no_cuenta == c.login select u;
                if(user.Count()>0)
                {
                    var cuenta = c.login;
                    var pass = c.password;
                    var al = from alumno in db.Alumnos from clave in db.Claves where cuenta == alumno.no_cuenta && cuenta == clave.login && pass == clave.password select alumno;
                    if (al.Count() > 0)
                    {
                        Session["loged"] = true;
                        Session["nombre"] = al.First().Nombre;
                        Session["userID"] = al.First().AlumnoId;
                        Session["noCuenta"] = al.First().no_cuenta;
                        Session["admin"] = false;
                        return Json("home");
                    }                    
                }
            }            
            return Json("error");
        }
        /*[HttpPost]
        public bool login(FormCollection form)
        {
            Base db = new Base();
            var cuenta = form["txtNoCuenta"];
            var pass = form["txtPass"];
            var login = from alumno in db.Alumnos from clave in db.Claves where cuenta == alumno.no_cuenta && cuenta == clave.login && pass == clave.password select (Alumno)alumno;
            if (login.Count() > 0)
            {

                Alumno a = login.First();
                Session["loged"] = true;
                Session["nombre"] = a.Nombre;
                Session["userID"] = a.AlumnoId;
                Session["noCuenta"] = a.no_cuenta;
                Response.Redirect(Url.Action("inicio", "home"));
                return true;
            }
            else
            {
                Session["loged"] = false;
                ViewBag.mensaje = "Usuario o contraseña incorrectos";
                Response.Redirect(Url.Action("index", "home"));

                return false;
            }
        }*/
        public ActionResult inicioAdmin()
        {
            return View();
        }
        public ActionResult inicio()
        {
            if (Session["loged"] != null)
            {
                if ((bool)Session["loged"])
                    return View();
                else
                    return View("Index");
            }
            return View("index");
        }
        [HttpPost]
        public void pass(FormCollection form)
        {
            var userID = form["txtUser"];
            var pass = form["txtPass"];
            Base bd = new Base();
            Clave c = new Clave();
            c.login = userID;
            c.password = pass;
            bd.Claves.Add(c);
            bd.SaveChanges();
            Response.Redirect(Url.Action("index", "home"));
        }

    }
}