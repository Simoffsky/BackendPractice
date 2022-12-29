using BackendPractice.View;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendPractice.Controllers; 


[ApiController]
[Route("Appointment")]
public class AppointmentController: ControllerBase {
    private readonly AppointmentService _service;

    public AppointmentController(AppointmentService service) {
        _service = service;
    }

    [HttpPost("add")]
    public ActionResult<AppointmentView> SaveAppointment(AppointmentView appointmentView) {
        var appointment = new Appointment(
            appointmentView.Id,
            appointmentView.StartTime,
            appointmentView.EndTime,
            appointmentView.PatientId,
            appointmentView.DoctorId
        );
        var res = _service.AddToConcreteDate(appointment);
        if (!res.Success)
            return Problem(statusCode: 404, detail: res.Error);
        
        return Ok(appointmentView);
    }
    
    [HttpPost("add_by_spec")]
    public ActionResult<AppointmentView> SaveAppointmentBySpec(DateTime dateTime, Specialization spec) {
        var res = _service.AddToConcreteDate(dateTime, spec);
        if (!res.Success)
            return Problem(statusCode: 404, detail: res.Error);

        var appointmentView = new AppointmentView {
            DoctorId = res.Value.DoctorId,
            Id = res.Value.Id,
            StartTime = res.Value.StartTime,
            EndTime = res.Value.EndTime
        };
        return Ok(appointmentView);
    }

    [HttpGet("get_free_by_spec")]
    public ActionResult<List<DateTime>> GetFreeBySpec(Specialization spec) {
        var res = _service.GetFreeBySpec(spec);
        if (!res.Success)
            return Problem(statusCode: 404, detail: res.Error);

        return Ok(res.Value);

    }
    
    [HttpGet("get_free_by_doctor")]
    public ActionResult<List<DateTime>> GetFreeByDoctor(Doctor doctor) {
        var res = _service.GetFreeByDoctor(doctor);
        if (!res.Success)
            return Problem(statusCode: 404, detail: res.Error);

        return Ok(res.Value);

    }
    
    
}