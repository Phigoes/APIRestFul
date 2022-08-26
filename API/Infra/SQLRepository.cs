using API.Entities;
using API.Infra.Data;
using API.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Infra
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext _dataContext;
        private DbSet<T> DbSet => _dataContext.Set<T>();

        public SQLRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Result<T> Get(int page, int quantity)
        {
            quantity = quantity == 0 ? 10 : quantity;
            var result = new Result<T>
            {
                Page = page,
                Quantity = quantity,
                Data = DbSet.Where(x => !x.Deleted)
                    .Skip((page - 1) * quantity)
                    .Take(quantity)
                    .AsNoTracking()
                    .ToList(),
            };

            result.Total = result.Data.Count();

            return result;
        }

        public T Get(string id) => DbSet.Where(x => !x.Deleted && x.Id == id).AsNoTracking().FirstOrDefault();

        public T Create(T entity)
        {
            entity.Id = Guid.NewGuid().ToString();

            _dataContext.Add(entity);
            _dataContext.Entry(entity).State = EntityState.Added;
            _dataContext.SaveChanges();

            return entity;
        }
        public void Update(string id, T entity)
        {
            _dataContext.Update(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
            _dataContext.SaveChanges();
        }

        public void Remove(string id)
        {
            var entity = Get(id);
            entity.Deleted = true;

            _dataContext.Update(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
            _dataContext.SaveChanges();
        }

        public T GetBySlug(string slug) => DbSet.Where(x => !x.Deleted && x.Slug == slug).AsNoTracking().FirstOrDefault();
    }
}
