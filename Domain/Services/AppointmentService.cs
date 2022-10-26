using Domain.Models;

namespace Domain.Services; 

public class AppointmentService {
	private IAppointmentRepository _repository;
	private IDoctorRepository _doctorRepository;
	public AppointmentService(IAppointmentRepository repo, IDoctorRepository doctorRepo) {
		_repository = repo;
		_doctorRepository = doctorRepo;
	}

	public Result<Appointment> AddToConcreteDate(Appointment appointment) {
		var doctor = _doctorRepository.Get(appointment.DoctorId);
		if (!_doctorRepository.Exists(doctor.Id))
			return Result.Fail<Appointment>("Doctor doesn't exists");
		
		if (!_repository.CheckFreeByDoctor(appointment.StartTime, doctor))
			return Result.Fail<Appointment>("Date with this doctor already taken");
		
		_repository.Create(appointment);
		return Result.Ok(appointment);
	}

	public Result<Appointment> AddToConcreteDate(DateTime dateTime, Specialization spec) {
		if (!_repository.CheckFreeBySpec(dateTime, spec))
			return Result.Fail<Appointment>("No free doctors for this spec/time");

		var appointment = _repository.CreateBySpec(dateTime, spec);
		return Result.Ok(appointment);
	}

	public Result<IEnumerable<Appointment>> GetFreeBySpec(Specialization spec) {
		var list = _repository.GetFreeBySpec(spec);
		return Result.Ok(list);
	}
	
	public Result<IEnumerable<Appointment>> GetFreeByDoctor(Doctor doctor) {
		if (!_doctorRepository.Exists(doctor.Id))
			return Result.Fail<IEnumerable<Appointment>>("Doctor doesn't exists");
		var list = _repository.GetFreeByDoctor(doctor);
		return Result.Ok(list);
	}
}