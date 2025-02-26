using System.ComponentModel.DataAnnotations;

namespace DotnetApiPostgres.Api.Models.DTO;

public class UpdatePersonDTO
{
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    public int Age { get; set; }

    public Gender Gender { get; set; }

    public static Person ToPerson(UpdatePersonDTO updatePersonDto)
    {
        return new Person
        {
            Id = updatePersonDto.Id,
            Name = updatePersonDto.Name,
            Age = updatePersonDto.Age,
            Gender = updatePersonDto.Gender
        };
    }
}
