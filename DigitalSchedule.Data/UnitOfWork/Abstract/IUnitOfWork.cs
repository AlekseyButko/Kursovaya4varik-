namespace DigitalSchedule.Data.UnitOfWork.Abstract;

public interface IUnitOfWork : IDisposable
{
    public IStudentRepository Students { get; set; }
    public ITeacherRepository Teachers { get; set; }
    public IUserRepository Users { get; set; }
    public ISubjectRepository Subjects { get; set; }
    void SaveChanges();
}
