namespace DigitalSchedule.Domain.Model;

public record UserLoginModel
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
