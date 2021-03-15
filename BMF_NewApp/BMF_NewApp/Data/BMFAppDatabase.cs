using BMF_NewApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static BMF_NewApp.Models.BMFAppTables;

namespace BMF_NewApp.Data
{
    public class BMFAppDatabase
    {
        readonly SQLiteAsyncConnection database;

        public BMFAppDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<TblUsersAccount>().Wait();
        }

        public Task<List<T>> GetItemsAsync<T>() where T : IEntity, new()
        {
            return database.Table<T>().ToListAsync();
        }

        public async Task<T> GetItem<T>(int id) where T : IEntity, new()
        {
            return await database.Table<T>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> InsertItem<T>(T item) where T : IEntity
        {
            return await database.InsertAsync(item);
        }

        public Task<int> SaveItemAsync<T>(T item) where T : IEntity
        {
            return database.UpdateAsync(item);
        }


        public Task<int> DeleteItemAsync<T>(T item)
        {
            return database.DeleteAsync(item);
        }
    }
}
