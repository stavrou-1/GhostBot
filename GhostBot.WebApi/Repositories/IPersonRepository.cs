using GhostBot.EntityModels;

namespace GhostBot.WebApi.Repositories
{
    public interface IPersonRepository
    {
        Task<Person?> CreateAsync(Person p);
        Task<Person[]> RetrieveAllAsync();
        Task<Person?> RetrieveAsync(string id);
        Task<Person?> UpdateAsync(Person p);
        Task<bool?> DeleteAsync(string id);
    }
}