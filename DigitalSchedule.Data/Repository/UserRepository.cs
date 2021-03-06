namespace DigitalSchedule.Data.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationContext dataContext) 
        : base(dataContext)
    {
    }
    public new IEnumerable<User> GetAll() => dataBase.Set<User>().ToList();
}
