using System;

namespace BusinessLayer.Interface
{
    /// <summary>
    /// Interface for the Greeting Business Logic Layer (BL).
    /// Defines the contract for greeting-related operations.
    /// </summary>
    public interface IGreetingBL
    {
        /// <summary>
        /// Retrieves a simple greeting message.
        /// </summary>
        /// <returns>A string containing the greeting message.</returns>
        string GetGreeting();
    }
}
