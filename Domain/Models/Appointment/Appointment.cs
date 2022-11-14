namespace Domain.Models; 

public class Appointment {
	public int Id { get; set; }
	public DateTime StartTime { get; set; }
	public DateTime EndTime { get; set; }
	public int PatientId { get; set; }
	public int DoctorId { get; set; }
	
	public Appointment(int id, DateTime startTime, DateTime endTime, int patientId, int doctorId) {
		Id = id;
		StartTime = startTime;
		EndTime = endTime;
		PatientId = patientId;
		DoctorId = doctorId;
	}
}