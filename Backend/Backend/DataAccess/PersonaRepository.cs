using Microsoft.EntityFrameworkCore;
using Backend.DataAccess.Interfaces;
using Backend.Entities;

namespace Backend.DataAccess
{
    /// <summary>
    /// Patrón 'Repository' para la entidad Persona
    /// </summary>
    public class PersonaRepository : IPersonaRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Persona>> GetAllAsync()
        {
            return await _context.Personas.ToListAsync();
        }

        public async Task<int> AddAsync(Persona persona)
        {
            await _context.Personas.AddAsync(persona);
            return await _context.SaveChangesAsync();
        }
    }
}
