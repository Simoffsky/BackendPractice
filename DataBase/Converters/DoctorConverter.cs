using Domain.Models;
using DataBase.Models;
namespace DataBase.Converters; 

public static class DoctorConverter {
    public static DoctorModel ToModel(this Doctor domainDoctor) {
        return new DoctorModel {
            Id = domainDoctor.Id,
            FullName = domainDoctor.FullName,
            Specialization = domainDoctor.Specialization.ToModel()
        };
    }
    
    public static Doctor ToDomain(this DoctorModel doctor) {
        return new Doctor(
            doctor.Id,
            doctor.FullName,
            doctor.Specialization.ToDomain()
        );
    }
}
