namespace Domain.Models; 

public interface IDoctorRepository : IRepository<Doctor>
{
    public Task<IEnumerable<Doctor>> GetBySpec(Specialization spec);
}