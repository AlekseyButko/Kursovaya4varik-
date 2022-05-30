﻿namespace DigitalSchedule.Domain.Model;

public record SubjectModel
{
    public Week Week { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public SubjectOrder Order { get; set; }
    public string Group { get; set; } = string.Empty;
    public StudySubgroup Subgroup { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TeacherName { get; set; } = string.Empty;
    public int AudienceNumber { get; set; }
}
