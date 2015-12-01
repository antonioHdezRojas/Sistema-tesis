using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STI.Models;

namespace STI.Controllers
{
    public class PreguntasController : Controller
    {
        private Base db = new Base();
        // GET: Preguntas       
        [HttpPost]
        public JsonResult guardar(Respuesta r)
        {
            r.AlumnoId = (int)Session["userID"];
            int aux = existe(r);
            if (aux < 0)
            {
                db.Respuestas.Add(r);
                db.SaveChanges();
                return Json("Respuesta guardada");
            }
            else
            {
                Respuesta res = db.Respuestas.Find(aux);
                db.Respuestas.Remove(res);
                db.Respuestas.Add(r);
                db.SaveChanges();
                return Json("Respuesta modificada");
            }

        }

        private int existe(Respuesta r)
        {
            var x = from resp in db.Respuestas where r.AlumnoId == resp.AlumnoId && r.PreguntaId == resp.PreguntaId select resp;
            if (x.Count() > 0)
                return x.Single().RespuestaId;
            else
                return -1;
        }
    }
}
