using Microsoft.EntityFrameworkCore;

namespace DataBase.Repositories; 
using Converters;
using Domain.Models;
public class DoctorRepository : IDoctorRepository {

    private readonly ApplicationContext _context;

    public DoctorRepository(ApplicationContext context) {
        _context = context;
    }
    
    public async Task<Doctor> Create(Doctor item) {
        await _context.Doctors.AddAsync(item.ToModel());
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<Doctor> Get(int id) {
        var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
        return doctor.ToDomain();
    }
    
    public async Task<IEnumerable<Doctor>> List() {
        var list = await _context.Doctors.Select(doctorModel => doctorModel.ToDomain()).ToListAsync();
        return list;
    }

    public async Task<bool> Exists(int id) {
        return await _context.Doctors.AnyAsync(d => d.Id == id);
    }

    public async Task<bool> Delete(int id) {
        var doctor =  await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
        if (doctor == default)
            return false; // Not deleted
        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();
        return true;
    }

    public bool IsValid(Doctor entity) {
        if (entity.Id < 0)
            return false;

        if (string.IsNullOrEmpty(entity.FullName))
            return false;

        return true;
    }

    public async Task<Doctor> Update(Doctor entity) {
        _context.Doctors.Update(entity.ToModel());
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<Doctor>> GetBySpec(Specialization spec) {
        var doctors = await _context.Doctors.
            Where(d =>  d.Specialization == spec.ToModel())
            .Select(d => d.ToDomain())
            .ToListAsync();
        return doctors;
    }
}