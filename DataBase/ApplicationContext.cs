using Microsoft.EntityFrameworkCore;
using DataBase.Models;
namespace DataBase;

public class ApplicationContext : DbContext
{
    public DbSet<UserModel> Users { get; set; }
    public DbSet<DoctorModel> Doctors { get; set; }
    public DbSet<AppointmentModel> Appointments { get; set; }
    public DbSet<ScheduleModel> Schedules { get; set; }
    public DbSet<SpecializationModel> Specializations { get; set; }
    public ApplicationContext(DbContextOptions options) : base(options) { }
}
