namespace DigitalSchedule.Data.Repository.Abstract;

public interface IRepository<TEntity>
{
    void Add(TEntity entity);
    IEnumerable<TEntity> GetAll();
    TEntity? Find(Predicate<TEntity> predicate);
    void Remove(TEntity entity);
}
