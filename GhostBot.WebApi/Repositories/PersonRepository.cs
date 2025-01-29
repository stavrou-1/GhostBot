using Microsoft.EntityFrameworkCore.ChangeTracking; //To use EntityEntry<T>
using GhostBot.EntityModels; //To use Person,Comment,Category
using Microsoft.Extensions.Caching.Memory; //To use IMemoryCache
using Microsoft.EntityFrameworkCore;
using GhostBot.DataContext; //To use ToArrayAsync.

namespace GhostBot.WebApi.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions _cacheEntryOptions = new()
        {
            SlidingExpiration = TimeSpan.FromMinutes(30)
        };

        //Use an instance data context field because it should not be
        //cached due to the data context having internal caching.
        private GhostBotContext _db;

        public PersonRepository(GhostBotContext db, IMemoryCache memoryCache)
        {
            _db = db;
            _memoryCache = memoryCache;
        }

        public async Task<Person?> CreateAsync(Person p)
        {
            p.PersonId = p.PersonId.ToUpper(); //Normalize to uppercase

            //Add to database using EF Core.
            EntityEntry<Person> added = await _db.Persons!.AddAsync(p);
            int affected = await _db.SaveChangesAsync();

            if (affected == 1)
            {
                //if saved to db then store in cache.
                _memoryCache.Set(p.PersonId, p, _cacheEntryOptions);
                return p;
            }

            return null;
        }

        public async Task<bool?> DeleteAsync(string id)
        {
            id = id.ToUpper();

            Person? p = await _db.Persons!.FindAsync(id);
            if (p is null) {
                return null;
            }

            _db.Persons.Remove(p);
            int affected = await _db.SaveChangesAsync();
            if (affected == 1)
            {
                _memoryCache.Remove(p.PersonId);
                return true;
            }
            return false;
        }

        public Task<Person[]> RetrieveAllAsync()
        {
            return _db.Persons!.ToArrayAsync();
        }

        public Task<Person?> RetrieveAsync(string id)
        {
            id = id.ToUpper(); //Normalize to uppercase;

            //Try to get from the Cache first.
            if (_memoryCache.TryGetValue(id, out Person? fromCache))
            {
                return Task.FromResult(fromCache);
            }

            //If not in the cache, then try to get it from the database.
            Person? fromDb = _db.Persons!.FirstOrDefault(p => p.PersonId == id);

            //If not -in db then return null result.
            if (fromDb is null) return Task.FromResult(fromDb);

            //if in the db, then store in the cache and return Person.
            _memoryCache.Set(fromDb.PersonId, fromDb, _cacheEntryOptions);
            return Task.FromResult(fromDb)!;
        }

        public async Task<Person?> UpdateAsync(Person p)
        {
            p.PersonId = p.PersonId.ToUpper();

            _db.Persons!.Update(p);
            int affected = await _db.SaveChangesAsync();
            if (affected == 1)
            {
                _memoryCache.Set(p.PersonId, p, _cacheEntryOptions);
                return p;
            }

            return null;
        }
    }
}