using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopSecretNicaAPICore.Helpers;
using TopSecretNicaAPICore.Models;
using TopSecretNicaAPICore.Repository.Interfaces;

namespace TopSecretNicaAPICore.Controllers
{
    [EnableCors("misReglasCores")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosRepository _usuariosRepository;

        public UsuariosController(IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Lista()
        {
            try
            {
                List<Usuarios> usuarios = _usuariosRepository.ObtenerUsuarios();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ResponseMessages.Ok, response = usuarios });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Obtener(int id)
        {
            try
            {
                Usuarios usuario = _usuariosRepository.ObtenerUsuarioPorId(id);
                if (usuario == null)
                {
                    return NotFound(new { mensaje = "Usuario no encontrado" });
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ResponseMessages.Ok, usuario = usuario });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Guardar([FromBody] Usuarios usuario)
        {
            try
            {
                bool resultado = _usuariosRepository.GuardarUsuario(usuario);
                if (resultado)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = ResponseMessages.Added});
                }
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "No se pudo agregar el usuario" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult Editar([FromBody] Usuarios usuario)
        {
            try
            {
                bool resultado = _usuariosRepository.EditarUsuario(usuario);
                if (resultado)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = ResponseMessages.Edited });
                }
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "No se pudo editar el usuario" });
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
                bool resultado = _usuariosRepository.EliminarUsuario(id);
                if (resultado)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = ResponseMessages.Deleted  });
                }
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "No se pudo eliminar el usuario" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
    }
}
