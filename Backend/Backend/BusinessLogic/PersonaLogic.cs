using AutoMapper;
using FluentValidation;
using Backend.BusinessLogic.Interfaces;
using Backend.DataAccess.Interfaces;
using Backend.DTOs;
using Backend.Entities;

namespace Backend.BusinessLogic
{
    public class PersonaLogic : IPersonaLogic
    {
        private readonly IMapper _mapper;
        private readonly IValidator<Persona> _validator;
        private readonly IPersonaRepository _repository;
        private readonly ILogger<PersonaLogic> _logger;

        public PersonaLogic(IMapper mapper, IValidator<Persona> validator, IPersonaRepository repository, ILogger<PersonaLogic> logger)
        {
            _mapper = mapper;
            _validator = validator;
            _repository = repository;
            _logger = logger;
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
                
                throw new ValidationException(validationResult.Errors);
            }

            // persistencia de los datos
            var id = await _repository.AddAsync(persona);

            return id;
        }
    }
}
