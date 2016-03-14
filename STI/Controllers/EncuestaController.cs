using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STI.Models;
using STI.Helpers;



namespace STI.Controllers
{
    public class EncuestaController : Controller
    {
        public const int PageSize1 = 5;
        public const int PageSize2 = 1;
        private List<Pregunta> preguntas(string encuesta)
        {
            Base db = new Base();
            var c = from categoria in db.Encuestas where categoria.Descripcion == encuesta select categoria.EncuestaID;            
                int cID = c.First();            
            var pre = from p in db.Preguntas where p.EncuestaID == cID select p;
            int[] orden = ordenar(pre.Count());
            Pregunta[] preguntas = pre.ToArray();
            List<Pregunta> lista = new List<Pregunta>();
            for (int i = 0; i < preguntas.Count(); i++)
            {
                var p = preguntas[orden[i]];
                var aux = from respuesta in db.Respuestas where respuesta.PreguntaId == p.PreguntaId select respuesta;
                if (aux.Count() == 0)
                    lista.Add(p);
            }
            int[,] ordenResp = new int[lista.Count, 4];
            int a = 0; //indice de ciclo
            foreach (var p in lista)
            {
                int[] aux = ordenar(4);
                for (int j = 0; j < 4; j++)
                    ordenResp[a, j] = aux[j];
                a++;
            }
            ViewBag.ordenResp = ordenResp;
            return lista;
        }
        public ActionResult autoestima()
        {
            return View(preguntas("Autoestima"));
        }
        public ActionResult comunicacion()
        {
            return View(preguntas("Oral y Escrito"));
        }
        public ActionResult habitosDeEstudio()
        {
            return View(preguntas("Habitos de Estudio"));
        }
        public ActionResult matematicas()
        {
            return View(preguntas("Matematicas"));

        }
        /*
        public ActionResult autoestima()
        {
            Base db = new Base();
            var c = from categoria in db.Encuestas where categoria.Descripcion == "Autoestima" select categoria.EncuestaID;
            int cID = c.First();
            var e = from pregunta in db.Preguntas where pregunta.EncuestaID == cID select pregunta;
            ViewBag.nPreguntas = e.Count();
            Session["encuesta"] = cID;
            int i;
            if (Session["inicio"] == null)
            {
                Session["inicio"] = 0;
                i = (int)Session["inicio"];
            }
            else
                i = (int)Session["inicio"];

            if (Session["orden"] == null)
                Session["orden"] = ordenar(e.Count());
            int aux = (int)Session["userID"];
            int[] orden = (int[])Session["orden"];
            Pregunta[] preguntas = e.ToArray();
            List<Pregunta> a = new List<Pregunta>();
            while (a.Count() < 5 && i < e.Count())
            {
                Pregunta p = preguntas[orden[i]];
                var res = from respuesta in db.Respuestas where p.PreguntaId == respuesta.PreguntaId && respuesta.AlumnoId == aux select respuesta;
                if (res.Count() > 0)
                    i++;
                else
                {
                    a.Add(preguntas[orden[i]]);
                    i++;
                }
            }
            Session["fin"] = i;
            i = (int)Session["inicio"];
            return View(a);
        }
        
        public void anterior()
        {
            // Session["fin"]
        }
        public void siguiente()
        {
            Base db = new Base();
            var cID = from cat in db.Encuestas select cat;
            int id = (int)Session["encuesta"];
            cID = cID.Where(x => x.EncuestaID == id);
            Session["inicio"] = (int)Session["fin"];
            string aux = cID.ToArray()[0].Descripcion;
            if (aux == "Autoestima")
                Response.Redirect(Url.Action("autoestima", "encuesta"));
            else if(aux == "Habitos de Estudio")
                Response.Redirect(Url.Action("habitosDeEstudio", "encuesta"));
        }
        public ActionResult habitosDeEstudio()
        {
            Base db = new Base();
            var c = from categoria in db.Encuestas where categoria.Descripcion == "Habitos de Estudio" select categoria.EncuestaID;
            int cID = c.First();
            var e = from pregunta in db.Preguntas where pregunta.EncuestaID == cID select pregunta;
            ViewBag.nPreguntas = e.Count();
            Session["encuesta"] = cID;
            int i;
            if (Session["inicio"] == null)
            {
                Session["inicio"] = 0;
                i = (int)Session["inicio"];
            }
            else
                i = (int)Session["inicio"];

            if (Session["orden"] == null)
                Session["orden"] = ordenar(e.Count());
            int aux = (int)Session["userID"];
            int[] orden = (int[])Session["orden"];
            Pregunta[] preguntas = e.ToArray();
            List<Pregunta> a = new List<Pregunta>();
            while (a.Count() < 5 && i < e.Count())
            {
                Pregunta p = preguntas[orden[i]];
                var res = from respuesta in db.Respuestas where p.PreguntaId == respuesta.PreguntaId && respuesta.AlumnoId == aux select respuesta;
                if (res.Count() > 0)
                    i++;
                else
                {
                    a.Add(preguntas[orden[i]]);
                    i++;
                }
            }
            Session["fin"] = i;
            i = (int)Session["inicio"];
            return View(a);
        }
        public void terminar()
        {
            Session["inicio"] = null;
            Session["encuesta"] = null;
            Session["fin"] = null;
            Session["orden"] = null;
            Response.Redirect(Url.Action("inicio", "Home"));
        }
        */
        private int[] ordenar(int lim)
        {
            int[] a;
            //se crea un vector de numeros aleatorios sin repeticion 
            a = Enumerable.Range(0, lim).OrderBy(x => Guid.NewGuid()).ToArray();
            return a;
        }
        [HttpPost]
        public JsonResult encuestaTerminada(Encuesta e)
        {
            Base db = new Base();
            List<Pregunta>p = preguntas(e.Descripcion);
            if (p.Count > 0)
                return Json("false");
            else
                return Json("true");
        }
        public ActionResult pager()
        {
            var preguntas = new PagedData<Pregunta>();
            using (var ctx = new Base())
            {                
                preguntas.Data = ctx.Preguntas.Take(PageSize1).ToList();
                preguntas.NumberOfPages = Convert.ToInt32(Math.Ceiling((Double)ctx.Preguntas.Count() / PageSize1));
                preguntas.CurrentPage = 1;
            }
            return View(preguntas);
        }
        public ActionResult cuerpoPrueba(int page)
        {
            var preguntas = new PagedData<Pregunta>();
            List<Pregunta> a = new List<Pregunta>();           
            using(var ctx = new Base())
            {
                a = ctx.Preguntas.ToList();
                preguntas.Data = a.Skip(PageSize1 * (page - 1)).Take(PageSize1);
                preguntas.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)ctx.Preguntas.Count() / PageSize1));
                preguntas.CurrentPage = page;
            }
            return PartialView(preguntas);
        }
    }
}