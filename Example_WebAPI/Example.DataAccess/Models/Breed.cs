using System.ComponentModel.DataAnnotations;

namespace Example.DataAccess.Models;

public class Breed
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MinLength(5)]
    [MaxLength(50)]
    public string Name { get; set; }
}