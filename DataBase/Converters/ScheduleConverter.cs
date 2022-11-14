using Domain.Models;
using DataBase.Models;
namespace DataBase.Converters; 

public static class ScheduleConverter {
    public static ScheduleModel ToModel(this Schedule schedule) {
        return new ScheduleModel {
            Id = schedule.Id,
            DoctorId = schedule.DoctorId,
            StartTime = schedule.StartTime,
            EndTime = schedule.EndTime
        };
    }

    public static Schedule ToDomain(this ScheduleModel scheduleModel) {
        return new Schedule(
            scheduleModel.Id,
            scheduleModel.DoctorId,
            scheduleModel.StartTime,
            scheduleModel.EndTime
        );
    }
}