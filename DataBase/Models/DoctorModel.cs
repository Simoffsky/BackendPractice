namespace DataBase.Models; 

public class DoctorModel {
    public int Id { get; set; }
    public string FullName { get; set; }
    public SpecializationModel Specialization { get; set; }
    
}