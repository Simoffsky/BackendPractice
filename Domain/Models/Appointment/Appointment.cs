namespace Domain.Appointment; 

public class Appointment {
	public DateTime StartTime;
	public DateTime EndTime;
	public int PatientId;
	public int DoctorId;
	
	public Appointment(DateTime startTime, DateTime endTime, int patientId, int doctorId) {
		StartTime = startTime;
		EndTime = endTime;
		PatientId = patientId;
		DoctorId = doctorId;
	}
}