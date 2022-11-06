using Domain.Models;
using Moq;
namespace UnitTests;

public class AppointmentServiceTests {
    private readonly AppointmentService _appointmentService;
    private readonly Mock<IDoctorRepository> _doctorRepository;
    private readonly Mock<IAppointmentRepository> _repository;


    public AppointmentServiceTests() {
        _repository = new Mock<IAppointmentRepository>();
        _doctorRepository = new Mock<IDoctorRepository>();
        _appointmentService = new AppointmentService(_repository.Object, _doctorRepository.Object);
    }

    // Test entities ~~
    public Doctor GetDoctor(string name = "John Doe") {
        
        return new Doctor(1, name, new Specialization(1, "Проктолог"));
    }

    public Specialization GetSpecialization() {
        return new Specialization(1, "Проктолог");
    }

    public Appointment GetAppointment() {
        return new Appointment(1, DateTime.Now, DateTime.Now, 1, 1);
    }

    [Fact]
    public void AddToConcreteDateByDoctorIsNotExists() {
        _doctorRepository.Setup(repo => repo.Exists(It.Is<int>(id => id == 1)))
            .Returns(false);
        _doctorRepository.Setup(repo => repo.Get(It.Is<int>(id => id == 1)))
            .Returns(GetDoctor());
        
        var response = _appointmentService.AddToConcreteDate(GetAppointment());
        
        Assert.False(response.Success);
        Assert.Equal("Doctor doesn't exists", response.Error);
    }
    [Fact]
    public void AddToConcreteDateByDoctorTimeTaken() {
        _doctorRepository.Setup(repo => repo.Exists(It.Is<int>(id => id == 1)))
            .Returns(true);
        _doctorRepository.Setup(repo => repo.Get(It.Is<int>(id => id == 1)))
            .Returns(GetDoctor());
        _repository.Setup(repo => repo.CheckFreeByDoctor(It.IsAny<DateTime>(), It.Is<Doctor>(doctor => doctor.Id == 1)))
            .Returns(false);

        var response = _appointmentService.AddToConcreteDate(GetAppointment());
        Assert.False(response.Success);
        Assert.Equal("Date with this doctor already taken", response.Error);
    }
    
    [Fact]
    public void AddToConcreteDateByDoctorOk() {
        _doctorRepository.Setup(repo => repo.Exists(It.Is<int>(id => id == 1)))
            .Returns(true);
        _doctorRepository.Setup(repo => repo.Get(It.Is<int>(id => id == 1)))
            .Returns(GetDoctor());
        _repository.Setup(repo => repo.CheckFreeByDoctor(It.IsAny<DateTime>(), It.Is<Doctor>(doctor => doctor.Id == 1)))
            .Returns(true);
        var response = _appointmentService.AddToConcreteDate(GetAppointment());
        
        Assert.True(response.Success);
    }

    [Fact]
    public void AddToConcreteDateBySpecNoFreeTime() {
        _repository.Setup(repo => repo.CheckFreeBySpec(It.IsAny<DateTime>(), It.IsAny<Specialization>()))
            .Returns(false);

        var response = _appointmentService.AddToConcreteDate(DateTime.Now, GetSpecialization());
        
        Assert.False(response.Success);
        Assert.Equal("No free doctors for this spec/time", response.Error);
    }

    [Fact]
    public void AddToConcreteDateBySpecOk() {
        _repository.Setup(repo => repo.CheckFreeBySpec(It.IsAny<DateTime>(), It.IsAny<Specialization>()))
            .Returns(true);
        
        var response = _appointmentService.AddToConcreteDate(DateTime.Now, GetSpecialization());
        
        Assert.True(response.Success);
    }

    [Fact]
    public void GetFreeBySpecOk() {
        var response = _appointmentService.GetFreeBySpec(GetSpecialization());
        Assert.True(response.Success);
    }

    [Fact]
    public void GetFreeByDoctorIsNotExists() {
        _doctorRepository.Setup(repo => repo.Exists(It.Is<int>(id => id == 1)))
            .Returns(false);
        
        var response = _appointmentService.GetFreeByDoctor(GetDoctor());
        
        Assert.False(response.Success);
        Assert.Equal("Doctor doesn't exists", response.Error);
    }
    
    [Fact]
    public void GetFreeByDoctorOk() {
        _doctorRepository.Setup(repo => repo.Exists(It.Is<int>(id => id == 1)))
            .Returns(true);
        
        var response = _appointmentService.GetFreeByDoctor(GetDoctor());
        
        Assert.True(response.Success);
    }
}