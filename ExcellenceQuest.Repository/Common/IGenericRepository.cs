namespace ExcellenceQuest.Repository.Common
{
    using ExcellenceQuest.Models.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    public interface IGenericRepository<TModel> 
    {
        Task<List<TModel>> GetAllAsync();
        Task<TModel> GetAsync(int id);
        //Task<TModel> GetAsync(Expression<Func<TEntity, bool>> predicate);
        //Task<List<TModel>> GetManyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TModel> AddAsync(TModel model);
        Task<TModel> UpdateAsync(TModel model);
        Task DeleteAsync(int id);
        Task DeleteAsync(TModel model);
    }
}