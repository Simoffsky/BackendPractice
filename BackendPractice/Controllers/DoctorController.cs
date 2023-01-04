using BackendPractice.View;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendPractice.Controllers; 

[ApiController]
[Route("doctor")]
public class DoctorController: ControllerBase {
    private readonly DoctorService _service;
    public DoctorController(DoctorService service) {
    _service = service;
    }
    
    [HttpPost("create")]
    public ActionResult<DoctorView> CreateDoctor(string name, Specialization spec) {
    Doctor doctor = new(0, name, spec);
        var res = _service.CreateDoctor(doctor);

        if (!res.Success)
            return Problem(statusCode: 404, detail: res.Error);

        return Ok(new DoctorView {
            DoctorId = doctor.Id,
            Name = doctor.FullName,
            Specialization = doctor.Specialization
        });
    }
    
    [HttpDelete("delete")]
    public ActionResult<DoctorView> DeleteDoctor(int id) {
    var res = _service.DeleteDoctor(id);

        if (!res.Success)
            return Problem(statusCode: 404, detail: res.Error);
        
        return Ok();
    }
    
    [HttpGet("get_all")]
    public ActionResult<List<DoctorView>> GetAllDoctors() {
        var res = _service.GetAll();
        List<DoctorView> doctors = new List<DoctorView>();
        foreach (var doc in res.Value) {
            var docView = new DoctorView {
                DoctorId = doc.Id,
                Name = doc.FullName,
                Specialization = doc.Specialization
            };
            doctors.Add(docView);
        }

        if (!res.Success)
            return Problem(statusCode: 404, detail: res.Error);
        
        return Ok(doctors);
    }
    
    [HttpGet("find")]
    public ActionResult<DoctorView> FindDoctor(int id) {
        var res = _service.GetById(id);

        if (!res.Success)
            return Problem(statusCode: 404, detail: res.Error);
        
        var doctor = res.Value;
        return Ok(new DoctorView {
            DoctorId = doctor.Id,
            Name = doctor.FullName,
            Specialization = doctor.Specialization
        });
    }
    
    [HttpGet("spec")]
    public ActionResult<List<DoctorView>> GetBySpec(Specialization spec) {
        var res = _service.GetBySpec(spec);
        List<DoctorView> doctors = new List<DoctorView>();
        foreach (var doc in res.Value) {
            var docView = new DoctorView {
                DoctorId = doc.Id,
                Name = doc.FullName,
                Specialization = doc.Specialization
            };
            doctors.Add(docView);
        }

        if (!res.Success)
            return Problem(statusCode: 404, detail: res.Error);
        
        return Ok(doctors);
    }
}