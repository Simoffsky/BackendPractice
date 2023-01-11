namespace Domain.Models; 

public interface IScheduleRepository: IRepository<Schedule> {
	Task<IEnumerable<Schedule>> GetScheduleByDate(Doctor doctor, DateOnly date);
}