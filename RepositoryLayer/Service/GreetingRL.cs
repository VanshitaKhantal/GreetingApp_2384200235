using ModelLayer.Entity;
using RepositoryLayer.Interface;
using System;
using RepositoryLayer;

namespace RepositoryLayer.Service
{
    /// <summary>
    /// Repository Layer class responsible for handling database operations related to greetings.
    /// Implements the IGreetingRL interface.
    /// </summary>
    public class GreetingRL : IGreetingRL
    {
        // Dependency injection of the database context
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor to initialize the repository layer with database context.
        /// </summary>
        /// <param name="context">Instance of AppDbContext for database access.</param>
        public GreetingRL(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Saves a greeting message into the database.
        /// </summary>
        /// <param name="greeting">The greeting message to be stored.</param>
        public void SaveGreeting(UserEntity greeting)
        {
            try
            {
                _context.Greetings.Add(greeting);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving greeting message", ex);
            }
        }

        /// <summary>
        /// Retrieves a greeting message by its unique ID.
        /// </summary>
        /// <param name="id">The unique ID of the greeting message.</param>
        /// <returns>The greeting entity if found, otherwise null.</returns>
        public UserEntity GetGreetingById(int id)
        {
            try
            {
                return _context.Greetings.FirstOrDefault(g => g.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving greeting message", ex);
            }
        }
    }
}
