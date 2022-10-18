namespace Domain.Models; 

public class Specialization {
    public int Id { get; set; }
    public string Name { get; set; }

    public Specialization(int id, string name) {
        Id = id;
        Name = name;
    }
}