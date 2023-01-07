using Domain.Models;

namespace Domain.Services;

public class DoctorService {
	private readonly IDoctorRepository _repository;

	public DoctorService(IDoctorRepository repository) {
		_repository = repository;
	}


	public async Task<Result<Doctor>> CreateDoctor(Doctor doctor) {
		if (string.IsNullOrEmpty(doctor.FullName))
			return Result.Fail<Doctor>("Invalid doctor FullName");

		if (await _repository.Exists(doctor.Id))
			return Result.Fail<Doctor>("Doctor already exists");
		
		await _repository.Create(doctor);
		return Result.Ok(doctor);
	}

	public async Task<Result> DeleteDoctor(int id) {
		if (!await _repository.Exists(id))
			return Result.Fail<Doctor>("Doctor doesn't exists");
		
		await _repository.Delete(id);
		return Result.Ok();
	}

	public async Task<Result<IEnumerable<Doctor>>> GetAll() {
		return Result.Ok(await _repository.List());
	}

	public async Task<Result<Doctor>> GetById(int id) {
		if (!await _repository.Exists(id))
			return Result.Fail<Doctor>("Doctor doesn't exists");
		
		return Result.Ok<Doctor>(await _repository.Get(id));
	}

	public async Task<Result<IEnumerable<Doctor>>> GetBySpec(Specialization spec) {
		return Result.Ok(await _repository.GetBySpec(spec));
	}
}