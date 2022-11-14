namespace DataBase.Models; 

public class AppointmentModel {
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
}