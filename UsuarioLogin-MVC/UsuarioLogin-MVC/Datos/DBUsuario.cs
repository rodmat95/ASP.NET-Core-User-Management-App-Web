using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using UsuarioLogin_MVC.Models;
using System.Data;
using System.Data.SqlClient;

namespace UsuarioLogin_MVC.Datos
{
    internal class DBUsuario
    {
        private static string CadenaSQL = "Data Source=(LocalDB)\\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\\UsuarioLogin.MDF; Integrated Security=True";

        public static bool Registrar(UsuarioDTO usuario)
        {
            bool result = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
                {
                    string query = "insert into Usuario(Nombre, Email, Clave, Restablecer, Confirmado, Token)";
                    query += " values(@Nombre, @Email, @Clave, @Restablecer, @Confirmado, @Token)";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Clave", usuario.Clave);
                    cmd.Parameters.AddWithValue("@Restablecer", usuario.Restablecer);
                    cmd.Parameters.AddWithValue("@Confirmado", usuario.Confirmado);
                    cmd.Parameters.AddWithValue("@Token", usuario.Token);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static UsuarioDTO Validar(string email, string clave)
        {
            UsuarioDTO usuario = null;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
                {
                    string query = "select Nombre,Restablecer,Confirmado from Usuario";
                    query += " where Email=@Email and Clave = @clave";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            usuario = new UsuarioDTO()
                            {
                                Nombre = dr["Nombre"].ToString(),
                                Restablecer = (bool)dr["Restablecer"],
                                Confirmado = (bool)dr["Confirmado"]

                            };
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return usuario;
        }

        public static UsuarioDTO Obtener(string email)
        {
            UsuarioDTO usuario = null;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
                {
                    string query = "select Nombre,Clave,Restablecer,Confirmado,Token from Usuario";
                    query += " where Email=@Email";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            usuario = new UsuarioDTO()
                            {
                                Nombre = dr["Nombre"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Restablecer = (bool)dr["Restablecer"],
                                Confirmado = (bool)dr["Confirmado"],
                                Token = dr["Token"].ToString(),
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return usuario;
        }

        public static bool RestablecerActualizar(int restablecer, string clave, string token)
        {
            bool result = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
                {
                    string query = @"update Usuario set " +
                        "Restablecer= @Restablecer, " +
                        "Clave= @Clave " +
                        "where Token= @Token";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@Restablecer", restablecer);
                    cmd.Parameters.AddWithValue("@Clave", clave);
                    cmd.Parameters.AddWithValue("@Token", token);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool Confirmar(string token)
        {
            bool result = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
                {
                    string query = @"update Usuario set " +
                        "Confirmado= 1 " +
                        "where Token= @Token";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@Token", token);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}