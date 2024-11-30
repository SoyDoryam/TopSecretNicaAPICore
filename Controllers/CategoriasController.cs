using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopSecretNicaAPICore.Helpers;
using TopSecretNicaAPICore.Models;
using TopSecretNicaAPICore.Repositories;

namespace TopSecretNicaAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriasRepository _categoriasRepository;

        // Inyección de dependencias para el repositorio
        public CategoriasController(ICategoriasRepository categoriasRepository)
        {
            _categoriasRepository = categoriasRepository;
        }

        // Obtener todas las categorías
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Lista()
        {
            try
            {
                var categorias = await _categoriasRepository.Obtener();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = categorias });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        // Obtener una categoría por ID
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var categorias = await _categoriasRepository.Obtener();
                var cat = categorias.Where(item => item.CategoriaID == id).FirstOrDefault();
                if (cat == null)
                {
                    return NotFound(new { mensaje = "Categoría no encontrada" });
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = cat });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        // Agregar una nueva categoría
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Guardar([FromBody] Categorias categoria)
        {
            try
            {
                var resultado = await _categoriasRepository.Agregar(categoria);
                if (resultado > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, new { mensaje = ResponseMessages.Added });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "No se pudo agregar la categoría" });
                }
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        // Modificar una categoría existente
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Editar([FromBody] Categorias categoria)
        {
            try
            {
                var resultado = await _categoriasRepository.Modificar(categoria);
                if (resultado > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = ResponseMessages.Edited });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "No se pudo actualizar la categoría" });
                }
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        // Eliminar una categoría por ID
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var resultado = await _categoriasRepository.Eliminar(id);
                if (resultado > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = ResponseMessages.Deleted});
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "No se pudo eliminar la categoría" });
                }
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
    }
}
