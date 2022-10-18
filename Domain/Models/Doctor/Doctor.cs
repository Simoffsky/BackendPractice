namespace Domain.Models; 

public class Doctor {
    public int Id { get; set; }
    public string FullName { get; set; }
    public Specialization Specialization { get; set; }

    public Doctor(int id, string name, Specialization spec) {
        Id = id;
        FullName = name;
        Specialization = spec;
    }
}