namespace DigitalSchedule.Data.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    public Repository(ApplicationContext dataContext) => dataBase = dataContext;

    protected readonly ApplicationContext dataBase;

    public void Add(TEntity entity) => dataBase.Set<TEntity>().Add(entity);

    public IEnumerable<TEntity> GetAll() => dataBase.Set<TEntity>().ToList();

    public void Remove(TEntity entity) => dataBase.Set<TEntity>().Remove(entity);

    public TEntity? Find(Predicate<TEntity> predicate) => dataBase.Set<TEntity>().ToList().Find(predicate);
}
