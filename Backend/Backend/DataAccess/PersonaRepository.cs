using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.DataAccess
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Persona persona)
        {
            await _context.Personas.AddAsync(persona);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Persona>> GetAllAsync()
        {
            return await _context.Personas.ToListAsync();
        }
    }
}
