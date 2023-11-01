using Payrolly.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.DAL.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        #region Add Mthods
        Task<bool> CreateAsync(TEntity entity);
        Task<bool> AddRangeAsync(IEnumerable<TEntity> entities);
        #endregion

        #region Updaet And Delete
        Task<bool> UpdateAsync(TEntity oldEntity, TEntity newEntity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> RemoveAsync(TEntity entity);
        //Task<bool> DetailsAsync(TEntity entity);
        Task<bool> RemoveRangeAsync(IEnumerable<TEntity> entities);
        #endregion

        #region Get Direct Data
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIDAsync<TId>(TId id);
        //Task<TEntity> GetByIDAsync(int id);
        #endregion

        #region Find with Filter
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, string[]? includes = null);
        Task<IQueryable<TEntity>> FindAllWithIncludes(string[]? includes);
        Task<IQueryable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, string[]? includes = null);
        #endregion

        #region Statiscal And Aggregate
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression);
        Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> expression);
        Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> expression);
        Task<decimal> MinAsync(Expression<Func<TEntity, decimal>> expression);
        Task<decimal> MaxAsync(Expression<Func<TEntity, decimal>> expression);
        #endregion
    }
}
