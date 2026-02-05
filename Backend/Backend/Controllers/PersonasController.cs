using AutoMapper;
using Backend.DataAccess;
using Backend.DTOs;
using Backend.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonaRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<Persona> _validator;
        private readonly ILogger<PersonasController> _logger;

        public PersonasController(IPersonaRepository repository, IMapper mapper, IValidator<Persona> validator, ILogger<PersonasController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todas las personas registradas en la base de datos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var personas = await _repository.GetAllAsync();
                var personasDto = _mapper.Map<IEnumerable<PersonaDTO>>(personas);
                return Ok(personasDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las personas.");
                return StatusCode(500, new { message = "Error procesando datos" });
            }
        }

        /// <summary>
        /// Recibe la información de una persona, la valida, la guarda y retorna un mensaje de éxito.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonaDTO personaDto)
        {
            try
            {
                // Mapeo de DTO a Entidad
                var persona = _mapper.Map<Persona>(personaDto);

                // Validación de la entidad
                var validationResult = await _validator.ValidateAsync(persona);

                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("Validación fallida para la persona con DNI: {DNI}. Errores: {Errors}", personaDto.DNI, string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                    return BadRequest(new { message = "Error procesando datos", errors = validationResult.Errors.Select(e => e.ErrorMessage) });
                }

                // Persistencia de los datos
                await _repository.AddAsync(persona);

                // Respuesta exitosa según consigna
                return Ok(new { message = $"Se recibió el nombre '{persona.Nombre}'" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar el registro de la persona con DNI: {DNI}", personaDto.DNI);
                // Manejo de errores básico según consigna
                return StatusCode(500, new { message = "Error procesando datos" });
            }
        }
    }
}
