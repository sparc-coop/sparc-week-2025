using Sparc.Blossom.Template.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sparc.Blossom.Template.Repositories
{
    public interface IDoctorRepository
    {
        Task AddAsync(Doctor doctor);
        Task<List<Doctor>> GetAllAsync();
        Task<Doctor> GetByIdAsync(int id);
    }
}
