namespace DigitalSchedule.Data.Context;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Student> Students { get; set; } = default!;
    public DbSet<Teacher> Teachers { get; set; } = default!;
    public DbSet<Subject> Subjects { get; set; } = default!;
}





























/*

Teachers.Add(new Teacher { Login = "901", Name = "Natalia", Surname = "Petrovna", Password = "901", Role = Domain.Enum.Role.Teacher });
        Teachers.Add(new Teacher { Login = "902", Name = "Galina", Surname = "Petrovna", Password = "902", Role = Domain.Enum.Role.Teacher });
        Teachers.Add(new Teacher { Login = "903", Name = "Galina", Surname = "Zaharovna", Password = "903", Role = Domain.Enum.Role.Teacher });
        Teachers.Add(new Teacher { Login = "904", Name = "Viktor", Surname = "Muza", Password = "904", Role = Domain.Enum.Role.Teacher });
        SaveChanges();

 */
