using System.ComponentModel.DataAnnotations;

namespace Example.DataAccess.Models;

public class Owner
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    public virtual List<Dog> Dogs { get; set; } = new List<Dog>();
}