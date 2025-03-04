using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Service
{
    /// <summary>
    /// Repository Layer class for handling Greeting messages.
    /// Implements the IGreetingRL interface to provide methods for saving greetings.
    /// </summary>
    public class GreetingRL : IGreetingRL
    {
        // In-memory list to store greeting messages
        private readonly List<GreetingModel> _greetings = new List<GreetingModel>();

        /// <summary>
        /// Saves a greeting message to the in-memory list.
        /// </summary>
        /// <param name="greetingModel">The greeting message object to be stored.</param>
        public void SaveGreeting(GreetingModel greetingModel)
        {
            // Adding the greeting message to the list
            _greetings.Add(greetingModel);
        }
    }
}
