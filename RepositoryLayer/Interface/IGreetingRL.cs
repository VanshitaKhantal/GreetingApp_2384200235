using ModelLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IGreetingRL
    {
        void SaveGreeting(UserEntity greeting);
        UserEntity GetGreetingById(int id); // New method to retrieve greeting by ID
    }
}
