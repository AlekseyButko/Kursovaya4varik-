namespace DigitalSchedule.Domain.Entity;

[Table("Teacher")]
public record Teacher : User
{
    public string Name { get; set; } = string.Empty;
}
