using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TopSecretNicaAPICore.Helpers;
using TopSecretNicaAPICore.Models;
using TopSecretNicaAPICore.Repository.Interfaces;

namespace TopSecretNicaAPICore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcasController : ControllerBase
    {
        private readonly IMarcasRepository _marcasRepository;

        public MarcasController(IMarcasRepository marcasRepository)
        {
            _marcasRepository = marcasRepository;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Lista()
        {
            try
            {
                var marcas = _marcasRepository.ObtenerMarcas();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = marcas });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = (object)null });
            }
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult Obtener(int id)
        {
            try
            {
                var marca = _marcasRepository.ObtenerMarcarPorId(id);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ResponseMessages.Ok, response = marca });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = (object)null });
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Guardar([FromBody] Marcas marcas)
        {
            try
            {
                bool estado = _marcasRepository.Guardar(marcas);
                if (estado)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = ResponseMessages.Added });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Error al guardar la marca." });
                }
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult Editar([FromBody] Marcas marcas)
        {
            try
            {
                bool estado = _marcasRepository.Editar(marcas);
                if (estado)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = ResponseMessages.Edited });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Error al editar la marca." });
                }
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public IActionResult Desactivar(int id)
        {
            try
            {
                bool estado = _marcasRepository.Desactivar(id);
                if (estado)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = ResponseMessages.Deleted });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Error al desactivar la marca." });
                }
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
    }
}
