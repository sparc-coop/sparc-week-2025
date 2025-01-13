using Microsoft.EntityFrameworkCore;
using Sparc.Blossom.Api;
using Sparc.Blossom.Template.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sparc.Blossom.Template.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Entities.Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Entities.Doctor>> GetAllAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<Entities.Doctor> GetByIdAsync(int id)
        {
            return await _context.Doctors.FindAsync(id);
        }
    }
}
