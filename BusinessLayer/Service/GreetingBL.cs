using ModelLayer.Entity;
using ModelLayer.Model;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;

namespace BusinessLayer.Service
{
    /// <summary>
    /// Business Logic Layer (BL) implementation for greeting service.
    /// </summary>
    public class GreetingBL : IGreetingBL
    {
        private readonly IGreetingRL _greeting;

        /// <summary>
        /// Constructor of Business Layer taht passes the reference of Interface
        /// </summary>
        /// <param name="greeting"></param>
        public GreetingBL(IGreetingRL greeting)
        {
            _greeting = greeting;
        }

        public void SaveGreeting(string message)
        {
            var greeting = new UserEntity { Message = message };
            _greeting.SaveGreeting(greeting);
        }

        /// <summary>
        /// Retrieves a greeting message by its ID from the repository layer.
        /// </summary>
        /// <param name="id">The unique ID of the greeting message.</param>
        /// <returns>The greeting entity if found, otherwise null.</returns>
        public UserEntity GetGreetingById(int id)
        {
            return _greeting.GetGreetingById(id);
        }

        /// <summary>
        /// Retrieves all greeting messages from the repository layer.
        /// </summary>
        /// <returns>List of all greeting messages.</returns>
        public List<UserEntity> GetAllGreetings()
        {
            return _greeting.GetAllGreetings();
        }

        /// <summary>
        /// Generates a personalized greeting message based on available user attributes.
        /// </summary>
        /// <param name="firstName">User's first name (optional).</param>
        /// <param name="lastName">User's last name (optional).</param>
        /// <returns>Personalized greeting message.</returns>
        public string GetGreeting(string? firstName = null, string? lastName = null)
        {
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return $"Hello, {firstName} {lastName}!";
            }
            if (!string.IsNullOrEmpty(firstName))
            {
                return $"Hello, {firstName}!";
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                return $"Hello, {lastName}!";
            }
            return "Hello World!";
        }
    }
}

