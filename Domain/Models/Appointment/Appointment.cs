using System.Data.Common;

namespace Domain.Appointment; 

public class Appointment {
	public int Id;
	public DateTime StartTime;
	public DateTime EndTime;
	public int PatientId;
	public int DoctorId;
	
	public Appointment(int id, DateTime startTime, DateTime endTime, int patientId, int doctorId) {
		Id = id;
		StartTime = startTime;
		EndTime = endTime;
		PatientId = patientId;
		DoctorId = doctorId;
	}
}