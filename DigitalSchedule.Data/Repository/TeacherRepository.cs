namespace DigitalSchedule.Data.Repository;

public class TeacherRepository : Repository<Teacher>, ITeacherRepository
{
    public TeacherRepository(ApplicationContext dataContext) 
        : base(dataContext)
    {
    }
}
