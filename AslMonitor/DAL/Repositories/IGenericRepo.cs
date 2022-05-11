using Microsoft.EntityFrameworkCore;

namespace AslMonitor.DAL.Repositories
{
    public interface IGenericRepo<TEntity> where TEntity : class //IEntity
    {
        IQueryable<TEntity> GetAll();
        TEntity? GetById(int id);
        TEntity? GetById(string id);
        TEntity? Create(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(int id);
        bool Delete(string id);
        TEntity? GetOneByRawSql(string query);
        IQueryable<TEntity?> GetAllByRawSql(string query);

        //Async Versions
        Task<List<TEntity?>> GetAllListAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity?> GetByIdAsync(string id);
        Task<TEntity?> CreateAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(string id);
        Task<bool> DeleteAsync(TEntity entity);

        Task<TEntity?> GetOneByRawSqlAsync(string query);
        Task<List<TEntity?>> GetAllByRawSqlAsync(string query);
    }

    public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : class //IEntity
    {
        //private readonly DatabaseContext _dbContext;

        //public GenericRepo(DatabaseContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        #region Sync versions
        public TEntity? GetByRawSQL(string whereClause)
        {
            using DatabaseContext _dbContext = new DatabaseContext();
            //var all = this.GetAll();
            var result = _dbContext.Set<TEntity>().FromSqlRaw($"Select * from {whereClause} ").FirstOrDefault();
            //var result = _dbContext.Set<TEntity>().Find(id);
            return result;
        }

        public IQueryable<TEntity> GetAll()
        {
            using DatabaseContext _dbContext = new DatabaseContext();
            return _dbContext.Set<TEntity>().AsNoTracking();
        }



        public TEntity? GetById(int id)
        {
            using DatabaseContext _dbContext = new DatabaseContext();
            var result = _dbContext.Set<TEntity>().Find(id);
            return result;
        }
        public TEntity? GetById(string id)
        {
            using DatabaseContext _dbContext = new DatabaseContext();
            var result = _dbContext.Set<TEntity>().Find(id);
            return result;
        }

        public TEntity? Create(TEntity entity)
        {
            using DatabaseContext _dbContext = new DatabaseContext();
            int result = 0;
            try
            {
                _dbContext.Set<TEntity>().Add(entity);
                //_dbContext.Set<TEntity>().Add(entity);
                result = _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return null;
            }

            if (result == 0) return null;
            //return (result > 0);
            return entity;
        }

        public bool Update(TEntity entity)
        {
            using DatabaseContext _dbContext = new DatabaseContext();
            int result = 0;
            try
            {
                _dbContext.Set<TEntity>().Update(entity);
                result = _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return (result > 0);
        }

        public bool Delete(int id)
        {
            using DatabaseContext _dbContext = new DatabaseContext();
            int result = 0;
            try
            {
                var entity = GetById(id);
                if (entity != null)
                {
                    _dbContext.Set<TEntity>().Remove(entity);
                    result = _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return (result > 0);
        }

        public bool Delete(string id)
        {
            using DatabaseContext _dbContext = new DatabaseContext();
            int result = 0;
            try
            {
                var entity = GetById(id);
                if (entity != null)
                {
                    _dbContext.Set<TEntity>().Remove(entity);
                    result = _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return (result > 0);
        }

        public TEntity? GetOneByRawSql(string query)
        {
            using DatabaseContext _dbContext = new DatabaseContext();
            IQueryable<TEntity> result = _dbContext.Set<TEntity>().FromSqlRaw(query);
            return result.FirstOrDefault();
        }

        public IQueryable<TEntity> GetAllByRawSql(string query)
        {
            using DatabaseContext _dbContext = new DatabaseContext();
            return _dbContext.Set<TEntity>().FromSqlRaw(query);
        }

        #endregion

        #region Async Versions
        public async Task<TEntity?> GetByRawSQLAsync(string whereClause)
        {
            using DatabaseContext _dbContext = new DatabaseContext();
            var result = await _dbContext.Set<TEntity>().FromSqlRaw($"Select * from {whereClause} ").FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<TEntity>> GetAllListAsync()
        {
            using DatabaseContext _dbContext = new DatabaseContext();
            return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }


        public async Task<TEntity?> GetByIdAsync(int id)
        {
            using DatabaseContext _dbContext = new DatabaseContext();
            var result = await _dbContext.Set<TEntity>().FindAsync(id);
            return result;
        }
        public async Task<TEntity?> GetByIdAsync(string id)
        {
            using DatabaseContext _dbContext = new DatabaseContext();
            var result = await _dbContext.Set<TEntity>().FindAsync(id);
            return result;
        }

        public async Task<TEntity?> CreateAsync(TEntity entity)
        {
            using DatabaseContext _dbContext = new DatabaseContext();
            int result = 0;
            try
            {
                await _dbContext.Set<TEntity>().AddAsync(entity);
                result = await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return null;
            }

            if (result == 0) return null;
            return entity;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            using DatabaseContext _dbContext = new DatabaseContext();
            int result = 0;
            try
            {
                var a = _dbContext.Set<TEntity>().Update(entity);
                result = await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return (result > 0);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using DatabaseContext _dbContext = new DatabaseContext();
            int result = 0;
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    _dbContext.Set<TEntity>().Remove(entity);
                    result = await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return (result > 0);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            using DatabaseContext _dbContext = new DatabaseContext();
            int result = 0;
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    _dbContext.Set<TEntity>().Remove(entity);
                    result = await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return (result > 0);
        }
        public async Task<bool> DeleteAsync(TEntity entity)
        {
            using DatabaseContext _dbContext = new DatabaseContext();
            int result = 0;
            try
            {
                //var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    _dbContext.Set<TEntity>().Remove(entity);
                    result = await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return (result > 0);
        }

        public async Task<TEntity?> GetOneByRawSqlAsync(string query)
        {
            using DatabaseContext _dbContext = new DatabaseContext();
            IQueryable<TEntity> result = _dbContext.Set<TEntity>().FromSqlRaw(query);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<List<TEntity?>> GetAllByRawSqlAsync(string query)
        {
            using DatabaseContext _dbContext = new DatabaseContext();
            return await _dbContext.Set<TEntity>().FromSqlRaw(query).ToListAsync();
        }

        #endregion
    }

}
