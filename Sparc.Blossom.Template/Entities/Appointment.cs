namespace Sparc.Blossom.Template.Entities;
public class Appointment : BlossomEntity<Guid>
{
    public Appointment(Guid doctorId, Guid patientId, DateTime appointmentDate, string notes)
    {
        Id = Guid.NewGuid();
        DoctorId = doctorId;
        PatientId = patientId;
        AppointmentDate = appointmentDate;
        Notes = notes;
        CreatedAt = DateTime.UtcNow;
        LastModifiedAt = DateTime.UtcNow;
    }

    public Guid DoctorId { get; set; }
    public Guid PatientId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }
}
