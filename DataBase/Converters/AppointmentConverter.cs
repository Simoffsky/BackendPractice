using Domain.Models;
using DataBase.Models;
namespace DataBase.Converters; 

public static class AppointmentConverter {
    public static AppointmentModel ToModel(this Appointment domainAppointment) {
        return new AppointmentModel {
            Id = domainAppointment.Id,
            StartTime = domainAppointment.StartTime,
            EndTime = domainAppointment.EndTime,
            PatientId = domainAppointment.PatientId,
            DoctorId = domainAppointment.DoctorId
        };
    }
    
    public static Appointment ToDomain(this AppointmentModel appointmentModel) {
        return new Appointment(
            appointmentModel.Id,
            appointmentModel.StartTime,
            appointmentModel.EndTime,
            appointmentModel.PatientId,
            appointmentModel.DoctorId
        );
    }
}