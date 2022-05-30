namespace DigitalSchedule.Domain.Entity;

[Table("Student")]
public record Student : User
{
    public string Group { get; set; } = string.Empty;
}
