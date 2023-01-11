using BackendPractice.View;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendPractice.Controllers; 


[ApiController]
[Route("Appointment")]
public class AppointmentController: ControllerBase {
    private readonly AppointmentService _service;
    private readonly DoctorService _doctorService;

    public AppointmentController(AppointmentService service, DoctorService doctorService) {
        _service = service;
        _doctorService = doctorService;
    }

    [Authorize]
    [HttpPost("add")]
    public async Task<ActionResult<AppointmentView>> SaveAppointment(AppointmentView appointmentView) {
        var appointment = new Appointment(
            appointmentView.Id,
            appointmentView.StartTime,
            appointmentView.EndTime,
            appointmentView.PatientId,
            appointmentView.DoctorId
        );
        var res = await _service.AddToConcreteDate(appointment);
        if (!res.Success)
            return Problem(statusCode: 404, detail: res.Error);
        
        return Ok(appointmentView);
    }
    
    [Authorize]
    [HttpPost("add_by_spec")]
    public async Task<ActionResult<AppointmentView>> SaveAppointmentBySpec(DateTime dateTime, Specialization spec) {
        var res = await _service.AddToConcreteDate(dateTime, spec);
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
    public async Task<ActionResult<List<DateTime>>> GetFreeBySpec(Specialization spec) {
        var res = await _service.GetFreeBySpec(spec);
        if (!res.Success)
            return Problem(statusCode: 404, detail: res.Error);

        return Ok(res.Value);

    }
    
    [HttpGet("get_free_by_doctor")]
    public async Task<ActionResult<List<DateTime>>> GetFreeByDoctor(int doctorId) {
        var doctorRes = await _doctorService.GetById(doctorId);
        if (!doctorRes.Success)
            return Problem(statusCode: 404, detail: doctorRes.Error);

        
        var res = await _service.GetFreeByDoctor(doctorRes.Value);
        if (!res.Success)
            return Problem(statusCode: 404, detail: res.Error);

        return Ok(res.Value);

    }
    
    
}