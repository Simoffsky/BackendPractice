using Domain.Models;

namespace Domain.Services; 

public class ScheduleService {
	private IScheduleRepository _repository;
	private IDoctorRepository _doctorRepository;

	public ScheduleService(IScheduleRepository repo, IDoctorRepository doctorRepo) {
		_repository = repo;
		_doctorRepository = doctorRepo;
	}

	public async Task<Result<IEnumerable<Schedule>>> GetByDoctor(Doctor doctor, DateOnly date) {
		if (!await _doctorRepository.Exists(doctor.Id))
			return Result.Fail<IEnumerable<Schedule>>("Doctor doesn't exists");
		if (!_doctorRepository.IsValid(doctor))
			return Result.Fail<IEnumerable<Schedule>>("Doctor is not invalid");

		return Result.Ok<IEnumerable<Schedule>>(await _repository.GetScheduleByDate(doctor, date));
	}

	public async Task<Result<Schedule>> Add(Schedule schedule) {
		if (!await _doctorRepository.Exists(schedule.DoctorId)) 
			return Result.Fail<Schedule>("Doctor doesn't exists");
		
		if(await _repository.Exists(schedule.Id))
			return Result.Fail<Schedule>("Schedule already exists");
		
		await _repository.Create(schedule);
		return Result.Ok<Schedule>(schedule);
	}

	public async Task<Result<Schedule>> Update(Schedule schedule) {
		if(!await _repository.Exists(schedule.Id))
			return Result.Fail<Schedule>("Schedule Doesn't exists");

		await _repository.Update(schedule);
		return Result.Ok<Schedule>(schedule);
	}

	public async Task<Result<Schedule>> Delete(Schedule schedule) {
		if(!await _repository.Exists(schedule.Id))
			return Result.Fail<Schedule>("Schedule Doesn't exists");
		
		await _repository.Delete(schedule.Id);
		return Result.Ok<Schedule>(schedule);
	}
}