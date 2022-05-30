using DigitalSchedule.Data.UnitOfWork.Abstract;

namespace DigitalSchedule.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(ApplicationContext context, 
        IStudentRepository students, 
        ITeacherRepository teachers,
        IUserRepository users,
        ISubjectRepository subjects)
    {
        dataContext = context;
        Students = students;
        Teachers = teachers;
        Users = users;
        Subjects = subjects;
    }

    private readonly ApplicationContext dataContext;

    public IStudentRepository Students { get; set; } = default!; // IoC initialized
    public ITeacherRepository Teachers { get; set; } = default!;
    public IUserRepository Users { get; set; } = default!;
    public ISubjectRepository Subjects { get; set; } = default!;

    public void SaveChanges() => dataContext.SaveChanges();
    public void Dispose() => dataContext.Dispose();
}
