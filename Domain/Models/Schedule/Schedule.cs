namespace Domain.Models; 

public class Schedule {
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    public Schedule(int id, int doctorId, DateTime startTime, DateTime endTime) {
        Id = id;
        DoctorId = doctorId;
        StartTime = startTime;
        EndTime = endTime;
    }
}