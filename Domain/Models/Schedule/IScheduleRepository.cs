namespace Domain.Models; 

public interface IScheduleRepository: IRepository<Schedule> {
	IEnumerable<Schedule> GetScheduleByDate(Doctor doctor, DateOnly date);
}