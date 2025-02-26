using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

using DotnetApiPostgres.Api.Models.DTO;

namespace DotnetApiPostgres.Api.Models;

[Table("Person")]
public class Person
{
    public int Id { get; set; }

    [Column(TypeName = "varchar(30)")]
    [Required]
    public required string Name { get; set; }

    [Required]
    [Range(0, 150)]
    public int Age { get; set; }

    [Required]
    public Gender Gender { get; set; }

    public static GetPersonDto ToGetPersonDto(Person person)
    {
        return new GetPersonDto
        {
            Id = person.Id,
            Name = person.Name,
            Age = person.Age,
            Gender = person.Gender
        };
    }
}