using DataBase.Converters;
using Domain.Models;

namespace DataBase.Repositories; 

public class ScheduleRepository: IScheduleRepository {
    private readonly ApplicationContext _context;

    public ScheduleRepository(ApplicationContext context) {
        _context = context;
    }
    

    public Schedule Create(Schedule item) {
        _context.Schedules.Add(item.ToModel());
        _context.SaveChanges();
        return item;
    }

    public Schedule? Get(int id) {
        return _context.Schedules.FirstOrDefault(s => s.Id == id).ToDomain();
    }
    public IEnumerable<Schedule> List() {
        return _context.Schedules.Select(scheduleModel => scheduleModel.ToDomain()).ToList();
    }

    public bool Exists(int id) {
        return _context.Schedules.Any(s => s.Id == id);
    }

    public bool Delete(int id) {
        var schedule = _context.Schedules.FirstOrDefault(s => s.Id == id);
        if (schedule == default)
            return false; // not deleted
        _context.Schedules.Remove(schedule);
        _context.SaveChanges();
        return true;
    }

    public bool IsValid(Schedule entity) {
        if (entity.Id < 0)
            return false;

        if (entity.StartTime >= entity.EndTime)
            return false;

        return true;
    }

    public Schedule Update(Schedule entity) {
        _context.Schedules.Update(entity.ToModel());
        _context.SaveChanges();
        return entity;
    }

    public IEnumerable<Schedule> GetScheduleByDate(Doctor doctor, DateOnly date) {
        return _context.Schedules.Where(s => 
                     s.DoctorId == doctor.Id && 
                     s.StartTime.Date == date.ToDateTime(new TimeOnly())) // Cast DateOnly -> DateTime
            .Select(s => s.ToDomain());                                   // with time - 00:00
    }
}