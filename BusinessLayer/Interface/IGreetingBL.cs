using RepositoryLayer.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IGreetingBL
    {
        string GetGreeting(string? firstName = null, string? lastName = null);
        Task<GreetingEntity> CreateGreeting(string message, int userId);
        Task<List<GreetingEntity>> GetGreetingsByUserId(int userId);
        Task<List<GreetingEntity>> GetAllGreetings();
        Task<bool> UpdateGreeting(int id, string newMessage);
        Task<bool> DeleteGreeting(int id);
        Task<bool> SaveGreeting(int userId, string message);
    }
}
