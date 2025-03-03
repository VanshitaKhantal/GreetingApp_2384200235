using BusinessLayer.Interface;
using System;

namespace BusinessLayer.Service
{
    /// <summary>
    /// Business Logic Layer (BL) implementation for greeting service.
    /// </summary>
    public class GreetingBL : IGreetingBL
    {
        /// <summary>
        /// Returns a simple greeting message.
        /// </summary>
        /// <returns>String containing "Hello World".</returns>
        public string GetGreeting()
        {
            return "Hello World";
        }
    }
}
