using Domain.Models;

namespace Domain.Appointment; 

public interface IAppointmentRepository : IRepository<Appointment> {
	IEnumerable<Appointment> GetFreeBySpec(Specialization spec);
	IEnumerable<Appointment> GetFree();
	
}