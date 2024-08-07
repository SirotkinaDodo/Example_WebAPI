using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Example.DataAccess.Models;

public class Dog
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MinLength(2)]
    [MaxLength(30)]
    public string Name { get; set; }
    
    [Required]
    [ForeignKey("Dog_BreedId_Key")]
    public Guid BreedId { get; set; }
    
    public virtual Breed Breed { get; set; }

    public virtual List<Owner> Owners { get; set; } = new List<Owner>();
}