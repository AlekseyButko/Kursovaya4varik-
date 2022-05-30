using Microsoft.EntityFrameworkCore;

namespace DigitalSchedule.Domain.Entity;

[Index(nameof(Login), IsUnique = true)]
public record User
{
    public int Id { get; private set; }
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set; }
}
