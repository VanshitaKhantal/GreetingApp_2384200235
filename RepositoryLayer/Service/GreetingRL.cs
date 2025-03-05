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

        /// <summary>
        /// Retrieves all greeting messages from the database.
        /// </summary>
        /// <returns>List of all greeting messages.</returns>
        public List<UserEntity> GetAllGreetings()
        {
            try
            {
                return _context.Greetings.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all greetings", ex);
            }
        }

        /// <summary>
        /// Updates an existing greeting message in the database.
        /// </summary>
        /// <param name="id">ID of the greeting to update.</param>
        /// <param name="newMessage">New message content.</param>
        /// <returns>True if updated successfully, false otherwise.</returns>
        public bool UpdateGreeting(int id, string newMessage)
        {
            try
            {
                var greeting = _context.Greetings.FirstOrDefault(g => g.Id == id);
                if (greeting == null)
                {
                    return false; // Greeting not found
                }

                greeting.Message = newMessage;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating greeting message", ex);
            }
        }

        /// <summary>
        /// Deletes an existing greeting message from the database.
        /// </summary>
        /// <param name="id">ID of the greeting to delete.</param>
        /// <returns>True if deleted successfully, false otherwise.</returns>
        public bool DeleteGreeting(int id)
        {
            try
            {
                var greeting = _context.Greetings.FirstOrDefault(g => g.Id == id);
                if (greeting == null)
                {
                    return false; // Greeting not found
                }

                _context.Greetings.Remove(greeting);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting greeting message", ex);
            }
        }
    }
}
