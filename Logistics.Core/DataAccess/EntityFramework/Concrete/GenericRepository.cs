using Logistics.Core.DataAccess.EntityFramework.Abstract;
using Logistics.Core.Entities.Concrete;
using Logistics.Core.Utilities.Results;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Logistics.Core.DataAccess.EntityFramework
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : BaseEntity, new()
    {
        protected readonly DbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public GenericRepository(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet.AsQueryable().Where(x => !x.IsDeleted && x.IsActive);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return DbSet.Where(x => !x.IsDeleted && x.IsActive).FirstOrDefault(filter);
        }

        public TEntity GetById(Guid id)
        {
            return DbSet.FirstOrDefault(x => x.Id == id && !x.IsDeleted && x.IsActive);
        }

        public IDataResult<TEntity> Add(TEntity entity)
        {
            entity.AddDate = DateTime.Now;

            var addedEntity = Context.Entry(entity);
            addedEntity.State = EntityState.Added;
            Context.SaveChanges();
            return new SuccessDataResult<TEntity>(addedEntity.Entity);
        }


        public IDataResult<TEntity> Update(TEntity entity)
        {
            entity.UpdateDate = DateTime.Now;

            var updatedEntity = Context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            Context.SaveChanges();
            return new SuccessDataResult<TEntity>(updatedEntity.Entity);
        }

        public IDataResult<TEntity> Delete(TEntity entity)
        {
            entity.DeleteDate = DateTime.Now;
            entity.IsDeleted = true;
            var deletedEntity = Context.Entry(entity);
            Context.SaveChanges();
            return new SuccessDataResult<TEntity>(deletedEntity.Entity);
        }

        public IDataResult<List<TEntity>> AddRange(List<TEntity> entities)
        {
            entities.ForEach(x =>
            {
                x.AddDate = DateTime.Now;
            });

            DbSet.AddRange(entities);
            Context.SaveChanges();
            return new SuccessDataResult<List<TEntity>>(entities);
        }
        public IDataResult<List<TEntity>> UpdateRange(List<TEntity> entities)
        {
            entities.ForEach(x =>
            {
                x.UpdateDate = DateTime.Now;
            });

            DbSet.UpdateRange(entities);
            Context.SaveChanges();
            return new SuccessDataResult<List<TEntity>>(entities);
        }

        public IDataResult<List<TEntity>> DeleteRange(List<TEntity> entities)
        {
            entities.ForEach(x =>
            {
                x.DeleteDate = DateTime.Now;
                x.IsDeleted = true;
            });

            DbSet.UpdateRange(entities);
            Context.SaveChanges();
            return new SuccessDataResult<List<TEntity>>(entities);
        }
        public bool Exist(Expression<Func<TEntity, bool>> filter)
        {
            return DbSet.Where(x => !x.IsDeleted && x.IsActive).Any(filter);
        }
    }
}