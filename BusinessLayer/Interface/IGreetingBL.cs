using ModelLayer.Entity;
using System;

public interface IGreetingBL
{
    /// <summary>
    /// Generates a personalized greeting message based on user attributes.
    /// </summary>
    /// <param name="firstName">User's first name (optional).</param>
    /// <param name="lastName">User's last name (optional).</param>
    /// <returns>Personalized greeting message.</returns>
    string GetGreeting(string? firstName = null, string? lastName = null);
    void SaveGreeting(string message);
    UserEntity GetGreetingById(int id); // New method
    List<UserEntity> GetAllGreetings(); // New method
    bool UpdateGreeting(int id, string newMessage);
}
