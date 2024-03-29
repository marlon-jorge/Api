﻿using Api.Bean;
using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    public class CompraLibroController : ApiController
    {
        [HttpPost]
        public string CompraLibro(Libro_bean cb)
        {
            return Libro_Model.CompraLibro(cb.corr);
        }


        [HttpDelete]
        public string EliminaCompraLibro(Cliente_bean cb)
        {
            return Libro_Model.EliminaCompraLibro(cb.corr);
        }
    }
}
