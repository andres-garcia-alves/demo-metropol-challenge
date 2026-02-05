using AutoMapper;
using Backend.Entities;

namespace Backend.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeo entre DTO y Entidad
            CreateMap<PersonaDTO, Persona>().ReverseMap();
        }
    }
}
