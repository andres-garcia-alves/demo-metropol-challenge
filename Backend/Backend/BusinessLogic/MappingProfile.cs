using AutoMapper;
using Backend.DTOs;
using Backend.Entities;

namespace Backend.BusinessLogic
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeo entre DTO y Entidad
            CreateMap<PersonaDTO, Persona>();
        }
    }
}
