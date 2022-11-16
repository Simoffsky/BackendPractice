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

	public Result<IEnumerable<DateTime>> GetFreeBySpec(Specialization spec) {
		var appointments = _repository.GetAllBySpec(spec);
		var list = ExcludeAppointments(appointments);
		return Result.Ok(list);
	}

	public Result<IEnumerable<DateTime>> GetFreeByDoctor(Doctor doctor) {
		if (!_doctorRepository.Exists(doctor.Id))
			return Result.Fail<IEnumerable<DateTime>>("Doctor doesn't exists");
		var appointments = _repository.GetAllByDoctor(doctor);
		var list = ExcludeAppointments(appointments);
		return Result.Ok(list);
	}

	private DateTime GetCurrentFormattedTime() {
		// discreted by half-hours timing (only hh:30 or hh:00)
		var time = DateTime.Now;
		if (time.Minute == 0)
			time = new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0);
		if (time.Minute <= 30)
			time = new DateTime(time.Year, time.Month, time.Day, time.Hour, 30, 0);
		else {
			time = time.AddHours(1);
			time = new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0);
		}

		return time;
	}

	private IEnumerable<DateTime> ExcludeAppointments(IEnumerable<Appointment> appointments) {
		var time = GetCurrentFormattedTime();
		var timeList = new List<DateTime>(); // list of free time
		var timeNow = DateTime.Now;
		while (time.Day == timeNow.Day) {
			if (appointments.All(a => time < a.StartTime || time > a.EndTime))
				timeList.Add(time);
			time = time.AddMinutes(30);
		}

		return timeList;
	}
}