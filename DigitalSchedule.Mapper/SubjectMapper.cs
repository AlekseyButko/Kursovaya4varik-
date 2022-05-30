using DigitalSchedule.Domain.Entity;
using DigitalSchedule.Domain.Model;

namespace DigitalSchedule.Mapper;

public static class SubjectMapper
{
    public static Subject ToDomain(this SubjectModel model)
    {
        return new Subject
        {
            AudienceNumber = model.AudienceNumber,
            DayOfWeek = model.DayOfWeek,
            Group = model.Group,
            Name = model.Name,
            Order = model.Order,
            Subgroup = model.Subgroup,
            TeacherName = model.TeacherName,
            Week = model.Week,
        };
    }
}
