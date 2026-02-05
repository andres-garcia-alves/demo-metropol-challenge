using Backend.Entities;

namespace Backend.DataAccess
{
    public interface IPersonaRepository
    {
        Task<int> AddAsync(Persona persona);
        Task<IEnumerable<Persona>> GetAllAsync();
    }
}
