using System.ComponentModel.DataAnnotations;

namespace ModelLayer.Entity
{
    public class UserEntity
    {
        [Key] // Primary Key
        public int Id { get; set; }

        [Required] // Ensures message is not null
        public string Message { get; set; }
    }
}
