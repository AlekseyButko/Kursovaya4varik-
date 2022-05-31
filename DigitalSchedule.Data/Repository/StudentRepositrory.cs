namespace DigitalSchedule.Data.Repository;

public class StudentRepositrory : Repository<Student>, IStudentRepository
{
    public StudentRepositrory(ApplicationContext dataContext) 
        : base(dataContext)
    {
    }

    public IEnumerable<Student> GetByGroup(string group) => dataBase.Students.Where(x => x.Group == group);
}
