using ModelLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IGreetingRL
    {
        void SaveGreeting(UserEntity greeting);
        UserEntity GetGreetingById(int id); // New method to retrieve greeting by ID
        List<UserEntity> GetAllGreetings(); // New method to list all greetings
        bool UpdateGreeting(int id, string newMessage); // New method to update greeting
    }
}
