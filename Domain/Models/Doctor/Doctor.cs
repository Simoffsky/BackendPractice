namespace Domain.Models; 

public class Doctor {
    public int Id { get; set; }
    public string FullName { get; set; }
    public Specialization Specialization { get; set; }
}