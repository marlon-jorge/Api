using Api.Bean;
using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    public class LibrosCController : ApiController
    {
        [HttpPost]
        public string InsertLibro(Libro_bean cb)
        {
            return Libro_Model.InsertarLibro(cb);
        }

        [HttpGet]
        public List<Libro_bean> ListaLibro()
        {            
            return Libro_Model.ConsultarLibro();
        }      

    }
}
