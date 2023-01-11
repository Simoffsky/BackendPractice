using Domain.Models;
using DataBase.Converters;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Repositories; 

public class AppointmentRepository: IAppointmentRepository {

    private readonly ApplicationContext _context;

    public AppointmentRepository(ApplicationContext context) {
        _context = context;
    }

    public async Task<Appointment> Create(Appointment item) {
        await _context.Appointments.AddAsync(item.ToModel());
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<Appointment> Get(int id) {
        var appointments = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
        return appointments.ToDomain();
    }

    public async Task<bool> Exists(int id) {
        return await _context.Appointments.AnyAsync(a => a.Id == id);
    }
    
    public async Task<IEnumerable<Appointment>> List() {
        return await _context.Appointments.Select(appointmentModel => appointmentModel.ToDomain()).ToListAsync();
    }

    public async Task<bool> Delete(int id) {
        var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
        if (appointment == default)
            return false; // not deleted
        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
        return true;
    }

    public bool IsValid(Appointment entity) {
        if (entity.Id < 0)
            return false;

        if (entity.StartTime >= entity.EndTime)
            return false;

        return true;
    }

    public async Task<Appointment> Update(Appointment entity) {
        _context.Appointments.Update(entity.ToModel());
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<Appointment>> GetAllBySpec(Specialization spec) {
        return await _context.Appointments.
            Where(a => _context.Doctors.
                Where(d => d.Specialization.Id == spec.Id).Any(d => a.DoctorId == d.Id))
            .Select(a => a.ToDomain())
            .ToListAsync();
        
    }

    public async Task<IEnumerable<Appointment>> GetAllByDoctor(Doctor doctor) {
        return await _context.Appointments.Where(a => a.DoctorId == doctor.Id)
            .Select(a => a.ToDomain())
            .ToListAsync();
    }

    public async Task<bool> CheckFreeBySpec(DateTime time, Specialization specialization) {
        var doctors = _context.Doctors
            .Where(d => d.Specialization.Id == specialization.Id);

        var appointments = _context.Appointments.Where(a => doctors.Any(d => d.Id == a.DoctorId));
        return await appointments.AnyAsync(a => time >= a.StartTime && time <= a.EndTime);
    }

    public Task<bool> CheckFreeByDoctor(DateTime time, Doctor doctor) {
        return _context.Appointments.AnyAsync(a => time >= a.StartTime && time <= a.EndTime && a.DoctorId == doctor.Id);
    }

    public Appointment CreateBySpec(DateTime dateTime, Specialization spec) {
        throw new NotImplementedException();
    }
}
