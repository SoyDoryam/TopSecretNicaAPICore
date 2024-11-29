using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using TopSecretNicaAPICore.Models;
using System.Security.Cryptography.X509Certificates;

namespace TopSecretNicaAPICore.Controllers
{
    [EnableCors("misReglasCores")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly string _connectionString;

        public UsuariosController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CadenaSQL");
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult Lista()
        {
            List<Usuarios> usuarios = new List<Usuarios>();

            try
            {
                using (var conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();
                    using(var cmd = new SqlCommand("sp_lista_usuarios", conexion))
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
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = usuarios });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = usuarios });
            }
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult Obtener(int id)
        {
            List<Usuarios> Lista = new List<Usuarios>();
            Usuarios usuario = new Usuarios();
            try
            {
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
                                Lista.Add(new Usuarios
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
                usuario = Lista.Where(item => item.UsuarioID == id).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", usuario = usuario });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, usuario = usuario });
            }
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult Guardar([FromBody]Usuarios usuarios)
            {
            try
            {
                using (var conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();
                    using (var cmd = new SqlCommand("sp_guardar_usuario", conexion))
                    {
                        cmd.Parameters.AddWithValue("Nombre", usuarios.Nombre);
                        cmd.Parameters.AddWithValue("Correo", usuarios.Correo);
                        cmd.Parameters.AddWithValue("contraseña", usuarios.Password);
                        cmd.Parameters.AddWithValue("RolID", usuarios.RolID);
                        cmd.Parameters.AddWithValue("Estado", usuarios.Estado);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();

                    }
                    conexion.Close();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "agregado"  });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult Editar([FromBody]Usuarios usuarios)
        {
            try
            {
                using (var conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();    
                    using (var cmd = new SqlCommand("sp_editar_usuario", conexion))
                    {
                        cmd.Parameters.AddWithValue("UsuarioID", usuarios.UsuarioID);
                        cmd.Parameters.AddWithValue("Nombre", usuarios.Nombre);
                        cmd.Parameters.AddWithValue("Correo", usuarios.Correo);
                        cmd.Parameters.AddWithValue("contraseña", usuarios.Password);
                        cmd.Parameters.AddWithValue("RolID", usuarios.RolID);
                        cmd.Parameters.AddWithValue("Estado", usuarios.Estado);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                    conexion.Close();
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Editado" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,new { mensaje = error.Message });
            }
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult Eliminar(int id)
        {
            try
            {
                using (var conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();
                    using (var cmd = new SqlCommand("sp_eliminar_usuario", conexion))
                    {
                        cmd.Parameters.AddWithValue("usuarioID", id);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                    conexion.Close();
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "eliminado" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
    }
}
