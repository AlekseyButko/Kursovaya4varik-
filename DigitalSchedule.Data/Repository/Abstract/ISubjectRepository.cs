namespace DigitalSchedule.Data.Repository.Abstract;

public interface ISubjectRepository : IRepository<Subject>
{
    IEnumerable<Subject> GetByGroup(string group);
    IEnumerable<Subject> GetByTeacher(string login);
    IEnumerable<Subject> GetByAudienceNumber(int number);
}
