namespace Domain.Models; 

public interface IAppointmentRepository : IRepository<Appointment> {
	IEnumerable<Appointment> GetAllBySpec(Specialization spec);
	IEnumerable<Appointment> GetAllByDoctor(Doctor doctor);
	bool CheckFreeBySpec(DateTime time, Specialization specialization);
	bool CheckFreeByDoctor(DateTime time, Doctor doctor);
	Appointment CreateBySpec(DateTime dateTime, Specialization spec);

}