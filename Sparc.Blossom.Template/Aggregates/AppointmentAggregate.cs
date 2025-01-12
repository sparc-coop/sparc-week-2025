using Sparc.Blossom.Template.Entities;

namespace Sparc.Blossom.Template.Aggregaters
{
    public class AppointmentAggregate(BlossomAggregateOptions<Appointment> options) : BlossomAggregate<Appointment>(options)
    {
        public BlossomQuery<Appointment> GetAppointmentsByUserIdAndType(Guid userId, Sparc.Blossom.Template.Common.UserType userType) =>
            Query().Where(x => (userType == Sparc.Blossom.Template.Common.UserType.Doctor ? x.DoctorId : x.PatientId) == userId);

        public BlossomQuery<Appointment> GetTodayAppointments() =>
            Query().Where(x => x.AppointmentDate.Date == DateTime.UtcNow.Date);
    }
}
