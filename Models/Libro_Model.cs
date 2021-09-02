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
    public class Libro_Model
    {
        private static ConectaSQL conexion = new ConectaSQL();
        ///inserta registro del libro ala bd
        public static string InsertarLibro(Libro_bean libro)
        {
            string messaje = "REGISTROS INSERTADOS CORRECTAMENTE";
            try
            {
                //ConectaSQL conexion = new ConectaSQL();
                SqlCommand coman = new SqlCommand();
                coman.Connection = conexion.AbrirConexion();
                coman.CommandText = "libro_SP";
                coman.CommandType = CommandType.StoredProcedure;
                coman.Parameters.Clear();
                coman.Parameters.AddWithValue("@nombre", libro.nombre);
                coman.Parameters.AddWithValue("@precio", (float)libro.precio);
                coman.Parameters.AddWithValue("@estado", libro.estado);
                coman.ExecuteNonQuery();
                conexion.CerrarConexion();
                return messaje;
            }
            catch (Exception ex)
            {
                return messaje = "Nose logro insetar el registro causa del error: " + ex.Message;
            }
        }

        ///consulta todos los registro del libro de la bd
        public static List<Libro_bean> ConsultarLibro()
        {
            List<Libro_bean> libros = new List<Libro_bean>();
            DataTable table = new DataTable();
            String sql = "select * from libros";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conexion.AbrirConexion());
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                foreach (DataRow fi in table.AsEnumerable())
                {
                    libros.Add(new Libro_bean
                    {
                        corr = (int)fi.ItemArray[0],
                        nombre = fi.ItemArray[1].ToString(),
                        precio = float.Parse(fi.ItemArray[2].ToString()),
                        estado = fi.ItemArray[3].ToString()
                    });
                }
                return libros;
            }
            else
            {                
                libros.Add(new Libro_bean { Mensaje = "No se encuentra ningun registos en la base de datos" }); ;
                return libros;
            }
        }

        /*////////////////////////////////////////////////////////////////////////////*/
        /*Metodo para Hacer la compra de la base de datos*/
        /*////////////////////////////////////////////////////////////////////////////*/
        public static string CompraLibro(int clienteCod)
        {
            string mensaje = "";
            try
            {
                //ConectaSQL conexion = new ConectaSQL();
                //SqlCommand coman = new SqlCommand();                
                DataTable table = new DataTable();
                DataTable table2 = new DataTable();
                String sql = "select nombre,apellido from cliente where corr=" + clienteCod;
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conexion.AbrirConexion());

                adapter.Fill(table);
                conexion.CerrarConexion();
                if (table.Rows.Count > 0)//si hay registro entra
                {
                    sql = "select * from libros where corr=" + clienteCod;
                    SqlDataAdapter adapter2 = new SqlDataAdapter(sql, conexion.AbrirConexion());
                    adapter2.Fill(table2);
                    if (table2.Rows.Count > 0)//si hay registros de libro entra
                    {
                        if (table2.Rows[0][3].ToString().Trim().ToUpper() == "DISPONIBLE")//cuando hay libros disponibles entra
                        {
                            string s = "','";
                            SqlCommand Cmd = conexion.AbrirConexion().CreateCommand();
                            adapter2.SelectCommand.CommandText = "insert into detalle values ('" + table.Rows[0][0].ToString() + s
                                                                           + table.Rows[0][1].ToString() + s
                                                                           + table2.Rows[0][1].ToString() + s
                                                                           + table2.Rows[0][3].ToString() + s
                                                                           + float.Parse(table2.Rows[0][2].ToString()) + "')";
                            Cmd.ExecuteNonQuery();
                            conexion.CerrarConexion();
                            mensaje = "libro comprado correctamente";
                        }
                        else
                        {
                            mensaje = "El libro que necesita ya no esta disponible";
                        }
                    }
                    else
                    {
                        mensaje = "El codigo del libro no existes";
                    }
                }
                else
                {
                    mensaje = "usted no esta en la base de datos de registros de clientes, por favor registrese";
                }
            }
            catch (Exception ex)
            {
                mensaje = "Consulte al desarrollador se produjo un error no controlado.: " + ex.Message;
            }
            return mensaje;
        }
    }
}