using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopSecretNicaAPICore.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Cors;

namespace TopSecretNicaAPICore.Controllers
{
    [EnableCors("misReglasCores")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly string cadenaSQL;

        public RolesController(IConfiguration configuration)
        {
            cadenaSQL = configuration.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Roles> roles = new List<Roles>();

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_lista_rol", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            roles.Add(new Roles 
                            { 
                                RolID = Convert.ToInt32(rd["RolID"]),
                                Codigo = rd["Codigo"].ToString(),
                                Nombre = rd["Nombre"].ToString(),
                                Descripcion = rd["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(rd["Estado"]),
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = roles });

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = roles });
            }
        }

        [HttpGet]
        [Route("[action]/{idRol:int}")]

        public IActionResult Obtener(int idRol) 
        {
            List<Roles> roles = new List<Roles>();
            Roles rol = new Roles();

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_lista_rol", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            roles.Add(new Roles
                            {
                                RolID = Convert.ToInt32(rd["RolID"]),
                                Codigo = rd["Codigo"].ToString(),
                                Nombre = rd["Nombre"].ToString(),
                                Descripcion = rd["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(rd["Estado"]),
                            });
                        }

                        rol = roles.Where(item => item.RolID == idRol).FirstOrDefault();

                        return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = rol});
                    }
                }
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = rol });
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Guardar([FromBody]Roles roles)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_guardar_rol", conexion);
                    cmd.Parameters.AddWithValue("Nombre", roles.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", roles.Descripcion);
                    cmd.Parameters.AddWithValue("Estado", roles.Estado);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje= "Agregado"});
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult Editar([FromBody]Roles roles)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_editar_rol", conexion);
                    cmd.Parameters.AddWithValue("RolID", roles.RolID == 0 ? DBNull.Value : roles.RolID);
                    cmd.Parameters.AddWithValue("Nombre", roles.Nombre is null ? DBNull.Value : roles.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", roles.Descripcion is null ? DBNull.Value : roles.Descripcion);
                    cmd.Parameters.AddWithValue("Estado", roles.Estado);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "editado" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    mensaje = error.Message
                });
            }
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult Eliminar(int rolId)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_eliminar_rol", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("RolID", rolId == 0 ? DBNull.Value : rolId);
                    cmd.ExecuteNonQuery();
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "eliminado" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    mensaje = error.Message
                });
            }
        }
    }
}
