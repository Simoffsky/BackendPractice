using Domain.Models;
using DataBase.Models;
namespace DataBase.Converters; 

public static class SpecializationConverter {
    public static SpecializationModel ToModel(this Specialization domainSpec) {
        return new SpecializationModel {
            Id = domainSpec.Id,
            Name = domainSpec.Name
        };
    }
    
    public static Specialization ToDomain(this SpecializationModel spec) {
        return new Specialization (
            spec.Id,
            spec.Name
        );
    }
}