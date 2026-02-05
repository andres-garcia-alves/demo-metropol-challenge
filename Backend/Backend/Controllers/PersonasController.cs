using Microsoft.AspNetCore.Mvc;
using Backend.BusinessLogic.Interfaces;
using Backend.DTOs;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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


                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("Validación fallida la persona con DNI: {DNI}. Errores: {Errors}",
                        personaDto.DNI, string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                    return BadRequest(new { message = "Error procesando datos", errors = validationResult.Errors.Select(e => e.ErrorMessage) });
                }

                // Persistencia de los datos
                await _repository.AddAsync(persona);

                // Respuesta exitosa
                return Ok(new { message = $"Se recibió el nombre '{persona.Nombre}'" }); // según consigna
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar la persona con DNI: {DNI}", personaDto.DNI);    // guardar en logs
                return StatusCode(500, new { message = "Error procesando datos" });                     // según consigna
            }
        }
    }
}
