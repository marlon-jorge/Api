using Api.Bean;
using Api.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Api.Models
{
    public class Cliente_Model
    {
        private static ConectaSQL conexion = new ConectaSQL();
        private static SqlCommand coman = new SqlCommand();
        public static string InsertarCliente(Cliente_bean cliente)
        {
            string messaje = "REGISTRO INSERTADO CORRECTAMENTE";            
            try
            {
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
            List<Cliente_bean> clientes = new List<Cliente_bean>();
            DataTable table = new DataTable();
            String sql = "select * from cliente where corr="+ clienteCod;
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

        public static string EliminaCliente(int clienteCod)
        {
            string mensaje = "";
            try
            {                
                String sql = "delete from cliente where corr=" + clienteCod;
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conexion.AbrirConexion());
                adapter.SelectCommand.ExecuteNonQuery();
                conexion.CerrarConexion();
                return mensaje="se elimino correctamente";
            } catch (Exception ex)
            {
                conexion.CerrarConexion();
                return mensaje="no se logro eliminar causa del error:" + ex.Message;
            }
        }
        

    }
}