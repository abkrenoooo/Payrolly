using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Payrolly.DAL.Data;
using Payrolly.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.DAL.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {

        #region Private Properties
        protected readonly ApplicationDbContext _context;
        private readonly ILogger<TEntity> _logger;
        private readonly DbSet<TEntity> dbSet;
        #endregion

        #region Constructor
        public GenericRepository(ApplicationDbContext context, ILogger<TEntity> logger)
        {
            _context = context;
            _logger = logger;
            this.dbSet = context.Set<TEntity>();
        }
        #endregion

        #region Methods For Create
        public async Task<bool> CreateAsync(TEntity entity)
        {
            try
            {
                await dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Adding a new {typeof(TEntity).Name} record");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return false;
            }
        }

        public async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                await dbSet.AddRangeAsync(entities);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Adding a new multiple {typeof(TEntity).Name} records");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return false;
            }
        }
        #endregion

        #region Update and Delete
        public async Task<bool> UpdateAsync(TEntity oldEntity, TEntity newEntity)
        {
            try
            {
                _context.Entry(oldEntity).CurrentValues.SetValues(newEntity);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Updaing {typeof(TEntity).Name} record");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            try
            {
                var entry = _context.Entry(entity);

                if (entry.State == EntityState.Modified)
                {
                    // get primary keys values for the entity
                    var keyProperties = _context.Model.FindEntityType(typeof(TEntity))!.FindPrimaryKey()!.Properties;
                    var keyPropertiesValues = keyProperties.Select(p => p.GetGetter().GetClrValue(entity))
                                                            .ToArray();

                    // search for existing entity
                    var existingEntity = await _context.Set<TEntity>().FindAsync(keyPropertiesValues);

                    // if the entity with primary keys is exist change properties values
                    if (existingEntity != null)
                    {
                        var existingEntry = _context.Entry(existingEntity);
                        existingEntry.CurrentValues.SetValues(entity);
                    }
                    // if the entity with primary keys is not exist attach new entity with new values
                    else
                    {
                        _context.Attach(entity);
                        entry.State = EntityState.Modified;
                    }

                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Updaing {typeof(TEntity).Name} record");

                    return true;
                }

                _logger.LogWarning($"Failed to update {typeof(TEntity).Name} record");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return false;
            }
        }

        public async Task<bool> RemoveAsync(TEntity entity)
        {
            try
            {
             
                dbSet.Remove(entity);

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                dbSet.RemoveRange(entities);

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Get Direct Data
        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            try
            {
                var query = await Task.FromResult(dbSet);
                _logger.LogInformation($"Getting all {typeof(TEntity).Name} records");

                return query;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return null!;
            }
        }

        public async Task<TEntity?> GetByIDAsync<TId>(TId id)
        {
            try
            {
                var entity = await dbSet.FindAsync(id);
                _logger.LogInformation($"Finding {typeof(TEntity).Name} record");

                return entity;
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return null;
            }
        }
        #endregion

        #region Find with Filter
        public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, string[]? includes = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<IQueryable<TEntity>> FindAllWithIncludes(string[]? includes)
        {
            IQueryable<TEntity> query = dbSet;

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query;
        }

        public async Task<IQueryable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, string[]? includes = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query.Where(predicate);
        }

        #endregion

        #region Statistics and Aggregate
        public async Task<int> CountAsync()
        {
            return await dbSet.CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await dbSet.CountAsync(expression);
        }

        public async Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> expression)
        {
            return await dbSet.SumAsync(expression);
        }

        public async Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> expression)
        {
            return await dbSet.AverageAsync(expression);
        }

        public async Task<decimal> MinAsync(Expression<Func<TEntity, decimal>> expression)
        {
            return await dbSet.MinAsync(expression);
        }

        public async Task<decimal> MaxAsync(Expression<Func<TEntity, decimal>> expression)
        {
            return await dbSet.MaxAsync(expression);
        }
        #endregion
    }
}
