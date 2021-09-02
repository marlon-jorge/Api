using Api.Bean;
using Api.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class Cliente_Model
    {
        public static string InsertarCliente(Cliente_bean cliente)
        {
            string messaje = "REGISTRO INSERTADO CORRECTAMENTE";            
            try
            {
                ConectaSQL conexion = new ConectaSQL();
                SqlCommand coman = new SqlCommand();
                coman.Connection = conexion.AbrirConexion();
                coman.CommandText = "cliente_SP";
                coman.CommandType = CommandType.StoredProcedure;
                coman.Parameters.Clear();
                coman.Parameters.AddWithValue("@nombre", cliente.nombre);
                coman.Parameters.AddWithValue("@apellido", cliente.apellido);
                coman.ExecuteNonQuery();
                conexion.CerrarConexion();
                return messaje;
            }
            catch (Exception ex)
            {
                return messaje = "Nose logro insetar el registro causa del error: " + ex.Message;               
            }
        }
        public static List<Cliente_bean> ConsultarCliente(int clienteCod)
        {
            ConectaSQL conexion = new ConectaSQL();
            SqlCommand coman = new SqlCommand();
            List<Cliente_bean> clientes = new List<Cliente_bean>();
            DataTable table = new DataTable();
            String sql = "select * from cliente";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conexion.AbrirConexion());
            adapter.Fill(table);
            
                foreach (DataRow fi in table.AsEnumerable())
                {
                    clientes.Add(new Cliente_bean
                    {
                        corr = (int)fi.ItemArray[0],
                        nombre = fi.ItemArray[1].ToString(),
                        apellido = fi.ItemArray[2].ToString()
                    });
                }                      
            return clientes;
        }
    
    }
}