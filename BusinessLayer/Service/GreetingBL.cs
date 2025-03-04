﻿using ModelLayer.Model;
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

        /// <summary>
        /// Saves a greeting message by creating a new GreetingModel instance 
        /// and passing it to the repository for storage.
        /// </summary>
        /// <param name="message">The greeting message to be saved.</param>
        public void SaveGreetingMessage(string message)
        {
            var greeting = new GreetingModel { Message = message };
            _greeting.SaveGreeting(greeting);
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

