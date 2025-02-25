using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetApiPostgres.Api.Models;

[Table("Person")]
public class Person
{
    public int Id { get; set; }

    [Column(TypeName = "varchar(30)")]
    [Required]
    public required string Name { get; set; }
}