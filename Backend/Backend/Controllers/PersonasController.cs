using Microsoft.AspNetCore.Mvc;
using Backend.BusinessLogic.Interfaces;
using Backend.DTOs;
using FluentValidation;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonaLogic _personaLogic;
        private readonly ILogger<PersonasController> _logger;

        public PersonasController(IPersonaLogic personaLogic, ILogger<PersonasController> logger)
        {
            _personaLogic = personaLogic;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todas las personas registradas en la base de datos
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Buscar los datos y mapear a DTOs
                var personasDto = await _personaLogic.GetAll();

                // Respuesta exitosa
                return Ok(personasDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las personas.");       // guardar en logs
                return StatusCode(500, new { message = "Error procesando datos" }); // según consigna
            }
        }

        /// <summary>
        /// Recibe la información de una persona, la valida, la guarda y retorna un mensaje de éxito
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonaDTO personaDto)
        {
            try
            {
                // Llamar a la lógica de negocio
                await _personaLogic.Create(personaDto);

                // Respuesta exitosa
                return Ok(new { message = $"Se recibió el nombre '{ personaDto.Nombre }'" }); // según consigna
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Any() ? ex.Errors.Select(e => e.ErrorMessage) : [ex.Message];
                return BadRequest(new { message = "Error al procesar los datos", errors });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar la persona con DNI: {DNI}", personaDto.DNI);    // guardar en logs
                return StatusCode(500, new { message = "Error al procesar los datos" });                // según consigna
            }
        }
    }
}
