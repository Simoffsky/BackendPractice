namespace DataBase.Repositories; 
using Converters;
using Domain.Models;
public class DoctorRepository : IDoctorRepository {

    private readonly ApplicationContext _context;

    public DoctorRepository(ApplicationContext context) {
        _context = context;
    }
    
    public Doctor Create(Doctor item) {
        _context.Doctors.Add(item.ToModel());
        return item;
    }

    public Doctor? Get(int id) {
        var doctor = _context.Doctors.FirstOrDefault(d => d.Id == id);
        return doctor?.ToDomain();
    }
    
    public IEnumerable<Doctor> List() {
        return _context.Doctors.Select(doctorModel => doctorModel.ToDomain()).ToList();
    }

    public bool Exists(int id) {
        return _context.Doctors.Any(d => d.Id == id);
    }

    public bool Delete(int id) {
        var doctor = _context.Doctors.FirstOrDefault(d => d.Id == id);
        if (doctor == default)
            return false; // Not deleted
        _context.Doctors.Remove(doctor);
        return true;
    }

    public bool IsValid(Doctor entity) {
        if (entity.Id < 0)
            return false;

        if (string.IsNullOrEmpty(entity.FullName))
            return false;

        return true;
    }

    public Doctor Update(Doctor entity) {
        _context.Doctors.Update(entity.ToModel());
        return entity;
    }

    public IEnumerable<Doctor> GetBySpec(Specialization spec) {
        return _context.Doctors.Where(d => d.Specialization == spec.ToModel()).Select(d => d.ToDomain());
    }
}