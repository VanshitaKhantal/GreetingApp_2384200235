using System.ComponentModel.DataAnnotations;

namespace ModelLayer.Entity
{
    /// <summary>
    /// Represents a user entity stored in the database.
    /// </summary>
    public class UserEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string? Message { get; set; }  // Nullable so it's optional
    }
}
