namespace Domain.Models; 

public interface IAppointmentRepository : IRepository<Appointment> {
	Task<IEnumerable<Appointment>> GetAllBySpec(Specialization spec);
	Task<IEnumerable<Appointment>> GetAllByDoctor(Doctor doctor);
	Task<bool> CheckFreeBySpec(DateTime time, Specialization specialization);
	Task<bool> CheckFreeByDoctor(DateTime time, Doctor doctor);
	Appointment CreateBySpec(DateTime dateTime, Specialization spec);

}