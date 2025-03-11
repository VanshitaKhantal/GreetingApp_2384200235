using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryLayer.Entity
{
    public class GreetingEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GreetingId { get; set; }
    [Required]
    public string Message { get; set; }

    // Foreign Key to UserEntity
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public virtual UserEntity User { get; set; }

    }
}

