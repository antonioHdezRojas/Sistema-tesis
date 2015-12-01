using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STI.Models;

namespace STI.Controllers
{
    public class ReportesController : Controller
    {

        // GET: Reportes                
        public ActionResult autoestima()
        {
            List<Respuesta> respuestas = getRepuestas("Autoestima");
            if (respuestas.Count() < 20)
            {
                ViewBag.mensaje = "Encuesta inconclusa";
                return View();
            }
            Base db = new Base();
            int userID = (int)Session["userID"];
            var resultado = from reporte in db.ResultadosEncAutoestima where reporte.AlumnoID == userID select reporte;
            int general = 0;
            int catGeneral = 0;
            int catEscolar = 0;
            int catFamilia = 0;
            if (resultado.Count() == 0)
            {                               
                foreach (var r in respuestas)
                {
                    var cat = from a in db.Preguntas where r.PreguntaId == a.PreguntaId select a;                                        
                    int? aux = cat.First().ClasificacionId;
                    switch (aux)
                    {
                        case 7:
                            catGeneral += r.valor;
                            break;
                        case 8:
                            catEscolar += r.valor;
                            break;
                        case 9:
                            catFamilia += r.valor;
                            break;
                    }                    
                }
                general = catEscolar + catFamilia + catGeneral;
                ResultadoEncAutoestima rEnc = new ResultadoEncAutoestima();
                rEnc.AlumnoID = userID;
                rEnc.Resultado = general;
                rEnc.ResultadoClEscolar = catEscolar;
                rEnc.ResultadoClFamilia = catFamilia;
                rEnc.ResultadoClGeneral = catGeneral;
                db.ResultadosEncAutoestima.Add(rEnc);
                db.SaveChanges();
            }
            else
            {
                general = resultado.Single().Resultado;
                catGeneral = resultado.Single().ResultadoClGeneral;
                catFamilia = resultado.Single().ResultadoClFamilia;
                catEscolar = resultado.Single().ResultadoClFamilia;
            }
            if (general > 53)
                ViewBag.mensaje = "Autoestima alta";
            else if(general > 26)
                ViewBag.mensaje = "Autoestima media";
            else
                ViewBag.mensaje = "Autoestima baja";        
            ViewBag.general = general;

            if (catGeneral > 19)
                ViewBag.mensajeCatG = "Autoestima alta";
            else if(catGeneral > 10)
                ViewBag.mensajeCatG = "Autoestima media";
            else
                ViewBag.mensajeCatG = "Autoestima baja";
            ViewBag.catGeneral = catGeneral;

            if (catEscolar > 19)
                ViewBag.mensajeCatE = "Autoestima alta";
            else if (catEscolar > 10)
                ViewBag.mensajeCatE = "Autoestima media";
            else
                ViewBag.mensajeCatE = "Autoestima baja";
            ViewBag.catFamilia = catFamilia;

            if (catFamilia > 16)
                ViewBag.mensajeCatF = "Autoestima alta";
            else if(catFamilia>8)
                ViewBag.mensajeCatF = "Autoestima media";
            else
                ViewBag.mensajeCatF = "Autoestima baja";
            ViewBag.catEscolar = catEscolar;

            return View();
        }
        public ActionResult habEstudio()
        {
            List<Respuesta> respuestas = getRepuestas("Habitos de Estudio");
            if (respuestas.Count() < 30)
            {
                ViewBag.mensaje = "Encuesta inconclusa";
                return View();
            }
            Base db = new Base();
            int userID = (int)Session["userID"];
            var resultado = from reporte in db.ResultadosEncHabEstudio where reporte.AlumnoID == userID select reporte;            
            int general = 0;
            int catConcentracion = 0;
            int catRelInterPer = 0;
            int catMemoria = 0;
            int catMotEst = 0;
            int catAdminTiempo = 0;
            int catPreEva = 0;
            if (resultado.Count() == 0)
            {                

                foreach (var r in respuestas)
                {
                    var cat = from a in db.Preguntas where r.PreguntaId == a.PreguntaId select a;
                    int? aux = cat.First().ClasificacionId;
                    switch (aux)
                    {
                        case 1:
                            catConcentracion += r.valor;
                            break;
                        case 2:
                            catRelInterPer += r.valor;
                            break;
                        case 3:
                            catMemoria += r.valor;
                            break;
                        case 4:
                            catMotEst += r.valor;
                            break;
                        case 5:
                            catAdminTiempo += r.valor;
                            break;
                        case 6:
                            catPreEva += r.valor;
                            break;
                    }
                }
                general = catAdminTiempo + catConcentracion + catMemoria + catMotEst + catPreEva + catRelInterPer;
                ResultadoEncHabEstudio rHab = new ResultadoEncHabEstudio();
                rHab.AlumnoID = userID;
                rHab.Resultado = general;
                rHab.ResultadoClConcentracion = catConcentracion;
                rHab.ResultadoClRelInterpesonales = catRelInterPer;
                rHab.ResultadoClMemoria = catMemoria;
                rHab.ResultadoClMotivacionEst = catMotEst;
                rHab.ResultadoClAdminTiempo = catAdminTiempo;
                rHab.ResultadoClPresentacionEva = catPreEva;
                db.ResultadosEncHabEstudio.Add(rHab);
                db.SaveChanges();
            }
            else
            {
                general = resultado.Single().Resultado;
                catConcentracion = resultado.Single().ResultadoClConcentracion;
                catRelInterPer = resultado.Single().ResultadoClRelInterpesonales;
                catMemoria = resultado.Single().ResultadoClMemoria;
                catMotEst = resultado.Single().ResultadoClMotivacionEst;
                catAdminTiempo = resultado.Single().ResultadoClAdminTiempo;
                catPreEva = resultado.Single().ResultadoClPresentacionEva;
            }
            if (general > 72)
                ViewBag.mensaje = "alto";
            ViewBag.general = general;      

            if (catConcentracion > 12)
                ViewBag.mensajeCatCon = "puedes enfrentar en forma exitosa problemas que generan tensión y angustia, logrando con ello concentración realmente efectiva frente a tus estudios. ";
            ViewBag.CatCon = catConcentracion;

            if (catRelInterPer > 12)
                ViewBag.mensajeCatRel = "te distingues por llevarte bien con la gente, estás en la mejor disposición para enfrentar cualquier reto y tratas a tus compañeros con respeto, los aceptas tal cual son y sabes defender tus derechos sin agresiones. ";
            ViewBag.catRel = catRelInterPer;

            if (catMemoria > 12)
                ViewBag.mensajeMem = "Te es fácil recordar la información y tienes una forma bien definida para darte cuenta cuando algo quedó aprendido, estás convendido/a de que el conocimiento previo es determinante para asimilar nueva información.";
            ViewBag.catMemoria = catMemoria;

            if (catMotEst > 12)
                ViewBag.mensajeCatMotEst = "aceptas la responsabilidad de los éxitos y fracasos, tomas decisiones y luchas para lograr mejorar, sabes identificar tus responsabilidades y libertades académicas universitarias.";
            ViewBag.catMotEst = catMotEst;

            if (catAdminTiempo > 12)
                ViewBag.mensajeCatAdmin = "sabes cuánto tiempo debes dedicarle a cada actividad y organizas muy bien tu día, cumples con tu programación y te caracteriza la autodisciplina.";
            ViewBag.catAdmin = catAdminTiempo;

            if (catPreEva > 12)
                ViewBag.mensajeCatEva = "consideras los exámenes como un periodo de razonamiento normal y te aseguras de entender las instrucciones.";
            ViewBag.catEva = catPreEva;

            return View();
        }
        public ActionResult mate()
        {
            List<Respuesta> respuestas = getRepuestas("Matematicas");
            if (respuestas.Count() < 20)
            {
                ViewBag.mensaje = "Encuesta inconclusa";
                return View();
            }
            Base db = new Base();
            int userID = (int)Session["userID"];
            var rep = from reporte in db.ResultadosEncMate where reporte.AlumnoID == userID select reporte;
            //int algebra = 0;
            //int aritmetica = 0;
            int general = 0;
            if (rep.Count() == 0)
            {                
                foreach(var r in respuestas)
                {
                    if (r.valor == 1)
                    {
                        /*switch ()
                        {
                            asigna categorias
                        }*/
                        general++;
                    }
                }   
            }
            else
            {
                general = rep.First().Resultado;
                //algebra = rep.First().ResultadoClAlgebra;
                //aritmetica = rep.First().ResultadoClAritmetica;
            }
            if (general < 10)
                ViewBag.mensaje = "No suficiente";
            else if(general < 14)
                ViewBag.mensaje = "Regular";
            else if(general<18)
                ViewBag.mensaje = "Bien";
            else
                ViewBag.mensaje = "Muy bien";
            ViewBag.resultado = general;
            return View();
        }
        public ActionResult comunicacion()
        {
            List<Respuesta> respuestas = getRepuestas("Oral y Escrito");
            Base db = new Base();
            if (respuestas.Count() < 20)
            {
                ViewBag.mensaje = "Encuesta inconclusa";
                return View();
            }
            int userID = (int)Session["userID"];
            var rep = from reporte in db.ResultadosEncComunicacion where reporte.AlumnoID == userID select reporte;            
            int general = 0;
            if (rep.Count() == 0)
            {
                foreach (var r in respuestas)
                {
                    if (r.valor == 1)                                            
                        general++;                    
                }
            }
            else            
                general = rep.First().Resultado;                            
            if (general < 11)
                ViewBag.mensaje = "No suficiente";
            else if (general < 14)
                ViewBag.mensaje = "Regular";
            else if (general < 18)
                ViewBag.mensaje = "Bien";
            else
                ViewBag.mensaje = "Muy bien";
            ViewBag.resultado = general;
            return View();
        }
        private List<Respuesta> getRepuestas(string encuesta)
        {
            Base db = new Base();
            int userID = (int)Session["userID"];
            var catID = from e in db.Encuestas where e.Descripcion == encuesta select e.EncuestaID;
            var preguntas = getPreguntas(catID.First());
            List<Respuesta> respuestas = new List<Respuesta>();
            foreach(var p in preguntas)
            {
                var resp = from r in db.Respuestas where r.PreguntaId == p.PreguntaId && r.AlumnoId ==userID  select r;
                if (resp.Count() > 0)
                    respuestas.Add(resp.First());
            }
            return respuestas;
        }        
        private List<Pregunta> getPreguntas(int catID)
        {
            Base db = new Base();
            var p = from preguntas in db.Preguntas where preguntas.EncuestaID == catID select preguntas;
            return p.ToList();
        }
    }
}