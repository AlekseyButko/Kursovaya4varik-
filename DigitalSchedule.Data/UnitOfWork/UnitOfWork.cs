using DigitalSchedule.Data.UnitOfWork.Abstract;

namespace DigitalSchedule.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(ApplicationContext contexts, 
        IStudentRepository studentRep, 
        ITeacherRepository teacherRep,
        IUserRepository userRep,
        ISubjectRepository subjectRep)
    {
        dataContext = contexts;
        Students = studentRep;
        Teachers = teacherRep;
        Users = userRep;
        Subjects = subjectRep;
    }

    private readonly ApplicationContext dataContext;

    public IStudentRepository Students { get; set; } = default!; // IoC initialized
    public ITeacherRepository Teachers { get; set; } = default!;
    public IUserRepository Users { get; set; } = default!;
    public ISubjectRepository Subjects { get; set; } = default!;

    public void SaveChanges() => dataContext.SaveChanges();
    public void Dispose() => dataContext.Dispose();
}
