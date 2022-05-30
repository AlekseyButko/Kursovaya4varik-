namespace DigitalSchedule.Data.Repository;

public class SubjectRepository : Repository<Subject>, ISubjectRepository
{
    public SubjectRepository(ApplicationContext dataContext) 
        : base(dataContext)
    {
    }

    public IEnumerable<Subject> GetByAudienceNumber(int number)
    {
        return dataBase.Subjects.Where(s => s.AudienceNumber == number).ToList();
    }

    public IEnumerable<Subject> GetByGroup(string group)
    {
        return dataBase.Subjects.Where(s => s.Group == group).ToList();
    }

    public IEnumerable<Subject> GetByTeacher(string login)
    {
        return dataBase.Subjects.Where(s => s.TeacherName == login).ToList();
    }
}
