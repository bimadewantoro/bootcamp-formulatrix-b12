namespace DotnetApiPostgres.Api.Models.DTO;

public class GetPersonDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }

    public static Person ToPerson(GetPersonDto getPersonDto)
    {
        return new Person
        {
            Id = getPersonDto.Id,
            Name = getPersonDto.Name,
            Age = getPersonDto.Age,
            Gender = getPersonDto.Gender
        };
    }
}
