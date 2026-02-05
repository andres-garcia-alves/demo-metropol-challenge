using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Backend.BusinessLogic.Interfaces;
using Backend.DataAccess.Interfaces;
using Backend.DTOs;
using Backend.Entities;
using FluentValidation;


namespace Backend.BusinessLogic
{
    public class PersonaLogic : IPersonaLogic
    {
        private readonly IMapper _mapper;
        private readonly IValidator<Persona> _validator;
        private readonly IPersonaRepository _repository;

        public PersonaLogic(IMapper mapper, IValidator<Persona> validator, IPersonaRepository repository)
        {
            _mapper = mapper;
            _validator = validator;
            _repository = repository;
        }

        public async Task<IEnumerable<PersonaDTO>> GetAll()
        {
            var personas = await _repository.GetAllAsync();
            var personasDto = _mapper.Map<IEnumerable<PersonaDTO>>(personas);

            // lógica adicional, si fuera necesaria
            // ...

            return personasDto;
        }

        public async Task<int> Create(PersonaDTO personaDto)
        {
            // mapeo de DTO a Entidad
            var persona = _mapper.Map<Persona>(personaDto);

            // validación de la entidad
            var validationResult = await _validator.ValidateAsync(persona);

            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validación fallida la persona con DNI: {DNI}. Errores: {Errors}",
                    personaDto.DNI, string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                return BadRequest(new { message = "Error procesando datos", errors = validationResult.Errors.Select(e => e.ErrorMessage) });
            }

            // persistencia de los datos
            var id = await _repository.AddAsync(persona);

            return id;
        }
    }
}
