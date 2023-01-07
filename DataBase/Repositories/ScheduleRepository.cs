using DataBase.Converters;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Repositories; 

public class ScheduleRepository: IScheduleRepository {
    private readonly ApplicationContext _context;

    public ScheduleRepository(ApplicationContext context) {
        _context = context;
    }
    

    public async Task<Schedule> Create(Schedule item) {
        await _context.Schedules.AddAsync(item.ToModel());
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<Schedule> Get(int id) {
        var schedule = await _context.Schedules.FirstOrDefaultAsync(s => s.Id == id);
        return schedule.ToDomain();
    }
    public async Task<IEnumerable<Schedule>> List() {
        return await _context.Schedules.Select(scheduleModel => scheduleModel.ToDomain()).ToListAsync();
    }

    public async Task<bool> Exists(int id) {
        return await _context.Schedules.AnyAsync(s => s.Id == id);
    }

    public async Task<bool> Delete(int id) {
        var schedule = await _context.Schedules.FirstOrDefaultAsync(s => s.Id == id);
        if (schedule == default)
            return false; // not deleted
        _context.Schedules.Remove(schedule);
        await _context.SaveChangesAsync();
        return true;
    }

    public bool IsValid(Schedule entity) {
        if (entity.Id < 0)
            return false;

        if (entity.StartTime >= entity.EndTime)
            return false;

        return true;
    }

    public async Task<Schedule> Update(Schedule entity) {
        _context.Schedules.Update(entity.ToModel());
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<Schedule>> GetScheduleByDate(Doctor doctor, DateOnly date) {
        return await _context.Schedules.Where(s => 
                     s.DoctorId == doctor.Id && 
                     s.StartTime.Date == date.ToDateTime(new TimeOnly())) // Cast DateOnly -> DateTime
            .Select(s => s.ToDomain()).ToListAsync();                     // with time - 00:00
    }
}