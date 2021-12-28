using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using NGS_Studio.Models;

namespace LocalDatabase
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
        }

        public Task<List<User>> GetUserAsync()
        {
            return _database.Table<User>().ToListAsync();
        }
        public Task<List<User>> GetBarbersAsync()
        {
            return _database.Table<User>().Where(User => User.IsBarber.Equals(true)).ToListAsync();
        }
        public Task<List<User>> GetClientsAsync()
        {
            return _database.Table<User>().Where(User => User.IsBarber.Equals(false)).ToListAsync();
        }
        public Task<List<User>> GetUserAsync(string phonenumber)
        {
            return _database.Table<User>().Where(User => User.PhoneNumber.Equals(phonenumber)).ToListAsync();
        }
        public Task<int> SaveUserAsync(User User)
        {
            return _database.InsertAsync(User);
        }
        public Task<int> UpdateUserAsync(User User)
        {
            return _database.UpdateAsync(User);
        }
    }
}