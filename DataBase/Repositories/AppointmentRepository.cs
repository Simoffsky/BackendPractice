using Domain.Models;
using DataBase.Converters;
using DataBase.Models;
namespace DataBase.Repositories; 

public class AppointmentRepository: IAppointmentRepository {

    private readonly ApplicationContext _context;

    public AppointmentRepository(ApplicationContext context) {
        _context = context;
    }

    public Appointment Create(Appointment item) {
        _context.Appointments.Add(item.ToModel());
        _context.SaveChanges();
        return item;
    }

    public Appointment? Get(int id) {
        return _context.Appointments.FirstOrDefault(a => a.Id == id).ToDomain();
    }

    public bool Exists(int id) {
        return _context.Appointments.Any(a => a.Id == id);
    }
    
    public IEnumerable<Appointment> List() {
        return _context.Appointments.Select(appointmentModel => appointmentModel.ToDomain()).ToList();
    }

    public bool Delete(int id) {
        var appointment = _context.Appointments.FirstOrDefault(a => a.Id == id);
        if (appointment == default)
            return false; // not deleted
        _context.Appointments.Remove(appointment);
        _context.SaveChanges();
        return true;
    }

    public bool IsValid(Appointment entity) {
        if (entity.Id < 0)
            return false;

        if (entity.StartTime >= entity.EndTime)
            return false;

        return true;
    }

    public Appointment Update(Appointment entity) {
        _context.Appointments.Update(entity.ToModel());
        _context.SaveChanges();
        return entity;
    }

    public IEnumerable<Appointment> GetAllBySpec(Specialization spec) {
        var doctors = _context.Doctors.Where(d => d.Specialization.Id == spec.Id);
        return _context.Appointments.Where(a => doctors.Any(d => a.DoctorId == d.Id))
            .Select(a => a.ToDomain())
            .ToList();
        
    }

    public IEnumerable<Appointment> GetAllByDoctor(Doctor doctor) {
        return _context.Appointments.Where(a => a.DoctorId == doctor.Id)
            .Select(a => a.ToDomain())
            .ToList();
    }

    public bool CheckFreeBySpec(DateTime time, Specialization specialization) {
        var doctors = _context.Doctors
            .Where(d => d.Specialization.Id == specialization.Id);

        var appointments = _context.Appointments.Where(a => doctors.Any(d => d.Id == a.DoctorId));
        return appointments.Any(a => time >= a.StartTime && time <= a.EndTime);
    }

    public bool CheckFreeByDoctor(DateTime time, Doctor doctor) {
        return _context.Appointments.Any(a => time >= a.StartTime && time <= a.EndTime && a.DoctorId == doctor.Id);
    }

    public Appointment CreateBySpec(DateTime dateTime, Specialization spec) {
        throw new NotImplementedException();
    }
}
