namespace ExcellenceQuest.Repository.Common
{
    using AutoMapper;
    using ExcellenceQuest.Repository.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using ExcellenceQuest.Models.Common;
    using System.Linq.Expressions;

    public class GenericRepository<TEntity, TModel> : IGenericRepository<TModel> where TEntity : class , ISoftDelete
    {
        protected readonly DbContext Context;
        protected readonly IMapper Mapper;

        public GenericRepository(DbContext context, IMapper mapper)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public virtual async Task<List<TModel>> GetAllAsync()
        {
            return Mapper.Map<List<TModel>>(await Context.Set<TEntity>().Where(e => !e.IsDeleted).ToListAsync());
        }

        public virtual async Task<TModel> GetAsync(int id)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);
            return Mapper.Map<TModel>(entity) ;
        }

        public virtual async Task<TModel> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            return  Mapper.Map<TModel>(entity);
        }

        public virtual async Task<List<TModel>> GetManyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await Context.Set<TEntity>().Where(predicate).Where(e => !e.IsDeleted).ToListAsync();
            return Mapper.Map<List<TModel>>(entities);
        }

        public virtual async Task<TModel> AddAsync(TModel model)
        {
            var entity = Mapper.Map<TEntity>(model);
            Context.Set<TEntity>().Add(entity);
            await Context.SaveChangesAsync();
            return Mapper.Map<TModel>(entity);
        }

        public virtual async Task<TModel> UpdateAsync(TModel model)
        {
            var entity = Mapper.Map<TEntity>(model);
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return Mapper.Map<TModel>(entity);
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {
                // Implement soft delete logic if desired (e.g., set IsDeleted property)
                // Alternatively, consider removing the entity from the context

                entity.IsDeleted = true;
                //Context.Set<TEntity>().Remove(entity);
                await Context.SaveChangesAsync();
            }
        }

        public virtual async Task DeleteAsync(TModel model)
        {
            var entity = Mapper.Map<TEntity>(model);
            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
        }

    }
}
