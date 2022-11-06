using Domain.Models;

namespace Domain.Services; 

public class ScheduleService {
	private IScheduleRepository _repository;
	private IDoctorRepository _doctorRepository;

	public ScheduleService(IScheduleRepository repo, IDoctorRepository doctorRepo) {
		_repository = repo;
		_doctorRepository = doctorRepo;
	}

	public Result<IEnumerable<Schedule>> GetByDoctor(Doctor doctor, DateOnly date) {
		if (!_doctorRepository.Exists(doctor.Id))
			return Result.Fail<IEnumerable<Schedule>>("Doctor doesn't exists");
		if (!_doctorRepository.IsValid(doctor))
			return Result.Fail<IEnumerable<Schedule>>("Doctor is not invalid");

		return Result.Ok<IEnumerable<Schedule>>(_repository.GetScheduleByDate(doctor, date));
	}

	public Result<Schedule> Add(Schedule schedule) {
		if (!_doctorRepository.Exists(schedule.DoctorId)) 
			return Result.Fail<Schedule>("Doctor doesn't exists");
		
		if(_repository.Exists(schedule.Id))
			return Result.Fail<Schedule>("Schedule already exists");
		
		_repository.Create(schedule);
		return Result.Ok<Schedule>(schedule);
	}

	public Result<Schedule> Update(Schedule schedule) {
		if(!_repository.Exists(schedule.Id))
			return Result.Fail<Schedule>("Schedule Doesn't exists");

		_repository.Update(schedule);
		return Result.Ok<Schedule>(schedule);
	}

	public Result<Schedule> Delete(Schedule schedule) {
		if(!_repository.Exists(schedule.Id))
			return Result.Fail<Schedule>("Schedule Doesn't exists");
		
		_repository.Delete(schedule.Id);
		return Result.Ok<Schedule>(schedule);
	}
}