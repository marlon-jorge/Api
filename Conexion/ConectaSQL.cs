using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Api.Conexion
{
    public class ConectaSQL
    {
        private SqlConnection Conexion = new SqlConnection("data source=DESKTOP-QF02LK0\\SQLEXPRESS;initial catalog=ConfiaTest;user id=sa;password=12345");

        public SqlConnection AbrirConexion()
        {
            if (Conexion.State == ConnectionState.Closed)
                Conexion.Open();
            return Conexion;
        }
        public SqlConnection CerrarConexion()
        {
            if (Conexion.State == ConnectionState.Open)
                Conexion.Close();
            return Conexion;
        }
    }
}