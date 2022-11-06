namespace Domain.Models; 

public interface IAppointmentRepository : IRepository<Appointment> {
	IEnumerable<DateTime> GetFreeBySpec(Specialization spec);
	IEnumerable<DateTime> GetFreeByDoctor(Doctor doctor);
	bool CheckFreeBySpec(DateTime time, Specialization specialization);
	bool CheckFreeByDoctor(DateTime time, Doctor doctor);
	Appointment CreateBySpec(DateTime dateTime, Specialization spec);

}