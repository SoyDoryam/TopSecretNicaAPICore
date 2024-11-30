using System.Data.SqlClient;
using System.Data;
using TopSecretNicaAPICore.Models;
using TopSecretNicaAPICore.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace TopSecretNicaAPICore.Repository
{
    public class RolesRepository : IRolesRepository
    {
        private readonly string _connectionString;

        public RolesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CadenaSQL");
        }

        public List<Roles> ObtenerRoles()
        {
            List<Roles> roles = new List<Roles>();

            using (var conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();
                var cmd = new SqlCommand("sp_lista_rol", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(new Roles
                        {
                            RolID = Convert.ToInt32(reader["RolID"]),
                            Codigo = reader["Codigo"].ToString(),
                            Nombre = reader["Nombre"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Estado = Convert.ToBoolean(reader["Estado"])
                        });
                    }
                }
            }

            return roles;
        }

        public Roles ObtenerRolPorId(int idRol)
        {
            Roles rol = null;

            using (var conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();
                var cmd = new SqlCommand("sp_lista_rol", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (Convert.ToInt32(reader["RolID"]) == idRol)
                        {
                            rol = new Roles
                            {
                                RolID = Convert.ToInt32(reader["RolID"]),
                                Codigo = reader["Codigo"].ToString(),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(reader["Estado"])
                            };
                        }
                    }
                }
            }

            return rol;
        }

        public bool GuardarRol(Roles rol)
        {
            try
            {
                using (var conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_guardar_rol", conexion);
                    cmd.Parameters.AddWithValue("Nombre", rol.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", rol.Descripcion);
                    cmd.Parameters.AddWithValue("Estado", rol.Estado);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditarRol(Roles rol)
        {
            try
            {
                using (var conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_editar_rol", conexion);
                    cmd.Parameters.AddWithValue("RolID", rol.RolID);
                    cmd.Parameters.AddWithValue("Nombre", rol.Nombre );
                    cmd.Parameters.AddWithValue("Descripcion", rol.Descripcion);
                    cmd.Parameters.AddWithValue("Estado", rol.Estado);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EliminarRol(int rolId)
        {
            try
            {
                using (var conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_eliminar_rol", conexion);
                    cmd.Parameters.AddWithValue("RolID", rolId == 0 ? DBNull.Value : rolId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
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
