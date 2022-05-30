namespace DigitalSchedule.Data.Repository.Abstract;

public interface IStudentRepository : IRepository<Student>
{
    IEnumerable<Student> GetByGroup(string group);
}
