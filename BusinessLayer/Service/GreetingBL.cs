namespace BusinessLayer.Service
{
    /// <summary>
    /// Business Logic Layer (BL) implementation for greeting service.
    /// </summary>
    public class GreetingBL : IGreetingBL
    {
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

