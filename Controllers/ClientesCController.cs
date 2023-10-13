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
    public class ClientesCController : ApiController
    {

        [HttpPost]
        public string InsertCliente(Cliente_bean cb)
        {
            return Cliente_Model.InsertarCliente(cb);
        }

        [HttpGet]        
        public List<Cliente_bean> ListaCliente(int cb)
        {            
            return Cliente_Model.ConsultarCliente(cb);

        }


        [HttpDelete]
        public string EliminaCliente(Cliente_bean cb)
        {
            return Cliente_Model.EliminaCliente(cb.corr);

        }
        public List<Cliente_bean> ListaCliente(Cliente_bean cb)
        {            
            return Cliente_Model.ConsultarCliente(cb.corr);
        }

    }
}
