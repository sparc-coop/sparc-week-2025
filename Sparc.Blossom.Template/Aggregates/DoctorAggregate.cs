using Sparc.Blossom.Template.Entities;
using Sparc.Blossom.Template.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sparc.Blossom.Template.Aggregaters
{
    public class DoctorAggregate
    {
        private readonly IDoctorRepository _doctorRepository;
        public DoctorAggregate(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        // Method to create a new doctor
        public async Task Create(Doctor doctor)
        {
            if (doctor == null)
                throw new ArgumentNullException(nameof(doctor));

            await _doctorRepository.AddAsync(doctor);
        }

        // Method to get all doctors (used for displaying a list of doctors)
        public async Task<List<Doctor>> GetAllDoctors()
        {
            return await _doctorRepository.GetAllAsync();
        }

        // Method to get a doctor by ID (for viewing details)
        public async Task<Doctor> GetDoctorById(int id)
        {
            return await _doctorRepository.GetByIdAsync(id);
        }
    }
}
