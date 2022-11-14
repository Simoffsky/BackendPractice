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
        return entity;
    }

    public IEnumerable<DateTime> GetFreeBySpec(Specialization spec) {
        var doctors = _context.Doctors.Where(d => d.Specialization.Id == spec.Id);
        var appointments = _context.Appointments.Where(a => doctors.Any(d => a.DoctorId == d.Id));
        return ExcludeAppointments(appointments);
    }

    public IEnumerable<DateTime> GetFreeByDoctor(Doctor doctor) {
        var appointments = _context.Appointments.Where(a => a.DoctorId == doctor.Id);
        return ExcludeAppointments(appointments);
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

    private DateTime getCurrentFormattedTime() { // discreted by half-hours timing (only hh:30 or hh:00)
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

    private IEnumerable<DateTime> ExcludeAppointments(IQueryable<AppointmentModel> appointments) {
        var time = getCurrentFormattedTime();
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