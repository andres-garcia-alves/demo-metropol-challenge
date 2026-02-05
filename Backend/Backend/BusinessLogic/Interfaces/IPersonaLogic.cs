using Backend.DTOs;

namespace Backend.BusinessLogic.Interfaces
{
    public interface IPersonaLogic
    {
        Task<IEnumerable<PersonaDTO>> GetAll();

        Task<int> Create(PersonaDTO personaDto);
    }
}