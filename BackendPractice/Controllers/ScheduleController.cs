using BackendPractice.View;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendPractice.Controllers; 

[ApiController]
[Route("schedule")]
public class ScheduleController: ControllerBase {
    private readonly ScheduleService _service;

    public ScheduleController(ScheduleService scheduleService) {
        _service = scheduleService;
    }

    [HttpGet("get_by_doctor")]
    public ActionResult<List<ScheduleView>> GetByDoctor(DoctorView doctor, DateOnly date) {
        var domainDoc = new Doctor(doctor.DoctorId, doctor.Name, doctor.Specialization);

        var res = _service.GetByDoctor(domainDoc, date);

        if (!res.Success)
            return Problem(statusCode: 404, detail: res.Error);

        List<ScheduleView> scheduleViews = new List<ScheduleView>();

        foreach (var schedule in res.Value) {
            scheduleViews.Add(new ScheduleView {
                Id = schedule.Id,
                DoctorId = schedule.DoctorId,
                EndTime = schedule.EndTime,
                StartTime = schedule.StartTime
            });
        }

        return Ok(scheduleViews);
    }

    [Authorize]
    [HttpPost("add")]
    public ActionResult<ScheduleView> AddSchedule(ScheduleView scheduleView) {
        var schedule = new Schedule(
            scheduleView.Id,
            scheduleView.DoctorId,
            scheduleView.StartTime,
            scheduleView.EndTime
        );

        var res = _service.Add(schedule);
        
        if(!res.Success)
            return Problem(statusCode: 404, detail: res.Error);

        return Ok(scheduleView);
    }
    
    [Authorize]
    [HttpPost("update")]
    public ActionResult<ScheduleView> UpdateSchedule(ScheduleView scheduleView) {
        var schedule = new Schedule(
            scheduleView.Id,
            scheduleView.DoctorId,
            scheduleView.StartTime,
            scheduleView.EndTime
        );

        var res = _service.Update(schedule);
        
        if(!res.Success)
            return Problem(statusCode: 404, detail: res.Error);

        return Ok(scheduleView);
    }
    
    [Authorize]
    [HttpDelete("delete")]
    public ActionResult<ScheduleView> DeleteSchedule(ScheduleView scheduleView) {
        var schedule = new Schedule(
            scheduleView.Id,
            scheduleView.DoctorId,
            scheduleView.StartTime,
            scheduleView.EndTime
        );

        var res = _service.Delete(schedule);
        
        if(!res.Success)
            return Problem(statusCode: 404, detail: res.Error);

        return Ok(scheduleView);
    }
}