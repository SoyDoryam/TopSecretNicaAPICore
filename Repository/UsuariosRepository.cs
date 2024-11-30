using System.Data.SqlClient;
using System.Data;
using TopSecretNicaAPICore.Models;
using TopSecretNicaAPICore.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace TopSecretNicaAPICore.Repository
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly string _connectionString;

        public UsuariosRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CadenaSQL");
        }

        public List<Usuarios> ObtenerUsuarios()
        {
            List<Usuarios> usuarios = new List<Usuarios>();

            using (var conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("sp_lista_usuarios", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(new Usuarios
                            {
                                UsuarioID = Convert.ToInt32(reader["UsuarioID"]),
                                Codigo = reader["Codigo"].ToString(),
                                Nombre = reader["Nombre"].ToString(),
                                Correo = reader["Correo"].ToString(),
                                RolID = Convert.ToInt32(reader["RolID"]),
                                FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                                Estado = Convert.ToBoolean(reader["Estado"]),
                            });
                        }
                    }
                }
            }

            return usuarios;
        }

        public Usuarios ObtenerUsuarioPorId(int id)
        {
            Usuarios usuario = null;

            using (var conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("sp_lista_usuarios", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (Convert.ToInt32(reader["UsuarioID"]) == id)
                            {
                                usuario = new Usuarios
                                {
                                    UsuarioID = Convert.ToInt32(reader["UsuarioID"]),
                                    Codigo = reader["Codigo"].ToString(),
                                    Nombre = reader["Nombre"].ToString(),
                                    Correo = reader["Correo"].ToString(),
                                    RolID = Convert.ToInt32(reader["RolID"]),
                                    FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                                    Estado = Convert.ToBoolean(reader["Estado"]),
                                };
                            }
                        }
                    }
                }
            }

            return usuario;
        }

        public bool GuardarUsuario(Usuarios usuario)
        {
            try
            {
                using (var conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();
                    using (var cmd = new SqlCommand("sp_guardar_usuario", conexion))
                    {
                        cmd.Parameters.AddWithValue("Nombre", usuario.Nombre);
                        cmd.Parameters.AddWithValue("Correo", usuario.Correo);
                        cmd.Parameters.AddWithValue("Contraseña", usuario.Password);
                        cmd.Parameters.AddWithValue("RolID", usuario.RolID);
                        cmd.Parameters.AddWithValue("Estado", usuario.Estado);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditarUsuario(Usuarios usuario)
        {
            try
            {
                using (var conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();
                    using (var cmd = new SqlCommand("sp_editar_usuario", conexion))
                    {
                        cmd.Parameters.AddWithValue("UsuarioID", usuario.UsuarioID);
                        cmd.Parameters.AddWithValue("Nombre", usuario.Nombre );
                        cmd.Parameters.AddWithValue("Correo", usuario.Correo);
                        cmd.Parameters.AddWithValue("contraseña", usuario.Password);
                        cmd.Parameters.AddWithValue("RolID", usuario.RolID);
                        cmd.Parameters.AddWithValue("Estado", usuario.Estado);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EliminarUsuario(int usuarioId)
        {
            try
            {
                using (var conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();
                    using (var cmd = new SqlCommand("sp_eliminar_usuario", conexion))
                    {
                        cmd.Parameters.AddWithValue("usuarioID", usuarioId);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
