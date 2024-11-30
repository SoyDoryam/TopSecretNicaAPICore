using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopSecretNicaAPICore.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Cors;
using TopSecretNicaAPICore.Repository.Interfaces;

namespace TopSecretNicaAPICore.Controllers
{
    [EnableCors("misReglasCores")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesRepository _rolesRepository;

        public RolesController(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Roles> roles;

            try
            {
                roles = _rolesRepository.ObtenerRoles();
                return Ok(new { mensaje = "ok", response = roles });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpGet]
        [Route("[action]/{idRol:int}")]
        public IActionResult Obtener(int idRol)
        {
            Roles rol;

            try
            {
                rol = _rolesRepository.ObtenerRolPorId(idRol);
                if (rol == null)
                {
                    return NotFound(new { mensaje = "Rol no encontrado" });
                }
                return Ok(new { mensaje = "ok", response = rol });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Guardar([FromBody] Roles roles)
        {
            try
            {
                bool resultado = _rolesRepository.GuardarRol(roles);
                if (resultado)
                {
                    return Ok(new { mensaje = "agregado" });
                }
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "No se pudo agregar el rol" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult Editar([FromBody] Roles roles)
        {
            try
            {
                bool resultado = _rolesRepository.EditarRol(roles);
                if (resultado)
                {
                    return Ok(new { mensaje = "editado" });
                }
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "No se pudo editar el rol" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult Eliminar(int rolId)
        {
            try
            {
                bool resultado = _rolesRepository.EliminarRol(rolId);
                if (resultado)
                {
                    return Ok(new { mensaje = "eliminado" });
                }
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "No se pudo eliminar el rol" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
    }
}
