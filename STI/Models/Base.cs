﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace STI.Models
{
    public class Base : DbContext
    {
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Encuesta> Encuestas { get; set; }
        public DbSet<Clasificacion> Clasificaciones { get; set; }
        public DbSet<Clave> Claves { get; set; }
        public DbSet<Respuesta> Respuestas { get; set; }
        public DbSet<Bachillerato> Bachilleratos { get; set; }
        public DbSet<Pregunta> Preguntas { get; set; }
        public DbSet<ResultadoEncAutoestima> ResultadosEncAutoestima { get; set; }
        public DbSet<ResultadoEncHabEstudio> ResultadosEncHabEstudio { get; set; }
        public DbSet<ResultadoEncComunicacion> ResultadosEncComunicacion { get; set; }
        public DbSet<ResultadoEncMate> ResultadosEncMate { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure Code First to ignore PluralizingTableName convention 
            // If you keep this convention, the generated tables  
            // will have pluralized names. 
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public class Alumno
    {
        public int AlumnoId { get; set; }
        public string no_cuenta { get; set; }
        public string Nombre { get; set; }
        public string Ap_pat { get; set; }
        public string Ap_mat { get; set; }
        public int BachilleratoId { get; set; }
        public float Promedio { get; set; }
        public int Ceneval { get; set; }
        //foreing keys
        public ICollection<Respuesta> Respuestas { get; set; }       
    }

    public class Profesor
    {
        public int ProfesorId { get; set; }
        public string Nombre { get; set; }
    }

    public class Encuesta
    {
        public int EncuestaID { get; set; }
        public string Descripcion { get; set; }
        //foreing keys       
        public ICollection<Pregunta> Preguntas { get; set; }
    }

    public class Clasificacion
    {
        public int ClasificacionId { get; set; }
        public int EncuestaID { get; set; }
        public string Descripcion { get; set; }
        //foreing Keys
        public Encuesta Encuesta { get; set; }
        public ICollection<Pregunta> Preguntas { get; set; }
    }

    public class Clave
    {
        public int ClaveId { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }

    public class Respuesta
    {
        public int RespuestaId { get; set; }
        public int PreguntaId { get; set; }
        public int AlumnoId { get; set; }
        public int valor { get; set; }
        //foreing keys
        public Alumno Alumno { get; set; }
        public Pregunta Pregunta { get; set; }
    }

    public class Bachillerato
    {
        public int BachilleratoId { get; set; }
        public string Nombre { get; set; }
    }

    public class Pregunta
    {
        public int PreguntaId { get; set; }
        public string Descripcion { get; set; }
        public int EncuestaID { get; set; }
        public int? ClasificacionId { get; set; }
        public string Respuesta1 { get; set; }
        public string Respuesta2 { get; set; }
        public string Respuesta3 { get; set; }
        public string Respuesta4 { get; set; }
        public string Respuesta5 { get; set; }
        public string Imagen { get; set; }
        //foreing Keys
        public Encuesta Encuesta { get; set; }
        public ICollection<Respuesta> Respuestas { get; set; }
        public Clasificacion Clasificacion { get; set; }
    }

    public class ResultadoEncAutoestima
    {
        public int ResultadoEncAutoestimaID { get; set; }
        public int AlumnoID { get; set; }
        public int ResultadoClGeneral { get; set; }
        public int ResultadoClEscolar { get; set; }
        public int ResultadoClFamilia { get; set; }
        public int Resultado { get; set; }
        //foreing Keys
        public Alumno Alumno { get; set; }
    }
    public class ResultadoEncHabEstudio
    {
        public int ResultadoEncHabEstudioID { get; set; }
        public int AlumnoID { get; set; }
        public int ResultadoClConcentracion { get; set; }
        public int ResultadoClRelInterpesonales { get; set; }
        public int ResultadoClMemoria { get; set; }
        public int ResultadoClMotivacionEst { get; set; }
        public int ResultadoClAdminTiempo { get; set; }
        public int ResultadoClPresentacionEva { get; set; }
        public int Resultado { get; set; }
        //foreing Keys
        public Alumno Alumno { get; set; }
    }
    public class ResultadoEncMate
    {
        public int ResultadoEncMateID { get; set; }
        public int AlumnoID { get; set; }   
        public int ResultadoClAritmetica { get; set; }
        public int ResultadoClAlgebra { get; set; }        
        public int Resultado { get; set; }
        //foreing Keys
        public Alumno Alumno { get; set; }
    }
    public class ResultadoEncComunicacion
    {
        public int ResultadoEncComunicacionID { get; set; }
        public int AlumnoID { get; set; }        
        public int Resultado { get; set; }
        //foreing Keys
        public Alumno Alumno { get; set; }
    }
}