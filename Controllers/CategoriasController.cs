using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TopSecretNicaAPICore.Models;

namespace TopSecretNicaAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly string _connectionString;

        public CategoriasController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Lista()
        {
            List<Categorias> categorias = new List<Categorias>();
            try
            {
                using (var conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();
                    using (var cmd = new SqlCommand("sp_lista_categorias", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (var rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                categorias.Add(new Categorias
                                {
                                    CategoriaID = Convert.ToInt32(rd["CategoriaID"]),
                                    Codigo = rd["Codigo"].ToString(),
                                    Nombre = rd["Nombre"].ToString(),
                                    Descripcion = rd["Descripcion"].ToString(),
                                });
                            }
                        }
                    }
                    conexion.Close();
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = categorias });
            }
            catch (Exception error )
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = categorias });
            }
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Obtener(int id)
        {
            List<Categorias> categorias = new List<Categorias>();
            Categorias categoria = new Categorias();
            try
            {
                using (var conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();
                    using (var cmd = new SqlCommand("sp_lista_categorias", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (var rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                categorias.Add(new Categorias
                                {
                                    CategoriaID = Convert.ToInt32(rd["CategoriaID"]),
                                    Codigo = rd["Codigo"].ToString(),
                                    Nombre = rd["Nombre"].ToString(),
                                    Descripcion = rd["Descripcion"].ToString(),
                                });
                            }
                        }
                    }
                    conexion.Close();
                }
                categoria = categorias.Where(item => item.CategoriaID == id).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = categoria });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = categoria });
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Guardar(Categorias categorias)
        {
            try
            {
                using (var conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();
                    using (var cmd = new SqlCommand("sp_guardar_categoria", conexion))
                    {
                        cmd.Parameters.AddWithValue("Nombre", categorias.Nombre);
                        cmd.Parameters.AddWithValue("Descripcion", categorias.Descripcion);
                        cmd.Parameters.AddWithValue("estado", categorias.estado);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteReader();
                    }
                    conexion.Close();
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "agregado"});
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,new { mensaje = error.Message});
            }
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult Editar(Categorias categoria)
        {
            try
            {
                using (var conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();
                    using (var cmd = new SqlCommand("sp_editar_categoria", conexion))
                    {
                        cmd.Parameters.AddWithValue("CategoriaID", categoria.CategoriaID == 0 ? DBNull.Value : categoria.CategoriaID);
                        cmd.Parameters.AddWithValue("Nombre", categoria.Nombre is null ? DBNull.Value : categoria.Nombre);
                        cmd.Parameters.AddWithValue("Descripcion", categoria.Descripcion is null ? DBNull.Value : categoria.Descripcion);
                        cmd.Parameters.AddWithValue("estado", categoria.estado);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteReader();
                    }
                    conexion.Close();
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "editado" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
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
                    using (var cmd = new SqlCommand("sp_eliminar_categoria", conexion))
                    {
                        cmd.Parameters.AddWithValue("CategoriaID", id);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteReader();
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
