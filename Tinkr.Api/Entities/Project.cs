using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tinkr.Api.Entities;

public class Project
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }
    [Required]
    [StringLength(200)]
    public required string Description { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public DateTime InitDate { get; set; }
    [Url]
    [StringLength(100)]
    public required string ImageUri { get; set; }

}

public class Student
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public DateTime Birthday { get; set; }
}