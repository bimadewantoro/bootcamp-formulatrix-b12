using System.ComponentModel.DataAnnotations;

namespace DotnetApiPostgres.Api.Models.DTO;

public class CreatePersonDTO
{
    [Required]
    public required string Name { get; set; }

    [Required]
    [Range(0, 150)]
    public int Age { get; set; }

    [Required]
    public Gender Gender { get; set; }

    public static Person ToPerson(CreatePersonDTO createPersonDto)
    {
        return new Person
        {
            Name = createPersonDto.Name,
            Age = createPersonDto.Age,
            Gender = createPersonDto.Gender
        };
    }
}
