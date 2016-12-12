using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyCoupleMobile.Providers.Interfaces;
using SQLite.Net;
using SQLite.Net.Async;
using SQLiteNetExtensionsAsync.Extensions;

namespace HappyCoupleMobile.Data
{
    public class BaseDao<T> : IBaseDao<T> where T : class, new()
    {
        private readonly ISqliteConnectionProvider _sqliteConnectionProvider;

        public BaseDao(ISqliteConnectionProvider sqliteConnectionProvider)
        {
            _sqliteConnectionProvider = sqliteConnectionProvider;
        }

        protected SQLiteAsyncConnection GetConnection()
        {
            return _sqliteConnectionProvider.GetConnection();
        }

        public async Task DeleteAllAsync()
        {
            SQLiteAsyncConnection connection = GetConnection();

            await connection.DeleteAllAsync<T>();
        }

        public async Task DeleteAsync(T entity)
        {
            SQLiteAsyncConnection connection = GetConnection();

            await connection.DeleteAsync<T>(entity);
        }

        public async Task<IList<T>> GetAllAsync()
        {
            SQLiteAsyncConnection connection = GetConnection();

            return await connection.Table<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            SQLiteAsyncConnection connection = GetConnection();

            return await connection.GetAsync<T>(id);
        }

        public async Task<IList<T>> GetWithChildrenAsync()
        {
            SQLiteAsyncConnection connection = GetConnection();

            var result = await connection.GetAllWithChildrenAsync<T>().ConfigureAwait(false);

            return result.ToList();
        }

        public async Task<T> GetFirstAsync()
        {
            SQLiteAsyncConnection connection = GetConnection();

            return await connection.Table<T>().FirstOrDefaultAsync();
        }

        public async Task<int> InsertAllAsync(IEnumerable<T> entities)
        {
            SQLiteAsyncConnection connection = GetConnection();

            return await connection.InsertAllAsync(entities);
        }

        public async Task<int> InsertAsync(T entity)
        {
            SQLiteAsyncConnection connection = GetConnection();

            return await connection.InsertAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            SQLiteAsyncConnection connection = GetConnection();

            await connection.UpdateAsync(entity);
        }
    }
}