using flashlightapi.DTOs;
using flashlightapi.Models;

namespace flashlightapi.Mappers;

public static class UserMapper
{
    public static UserDTO ToUserDto(this User dbUser)
    {
        return new UserDTO()
        {
            Id = dbUser.Id,
            Name = dbUser.Name,
            Email = dbUser.Email,
            Source = dbUser.Source,
            Assignments = dbUser.Assignments.Select(u => u.ToAssignmentDTO()).ToList(),
        };
    }

    public static User ToUserModel(this UserDTO userDto)
    {
        return new User()
        {
            Name = userDto.Name,
            Email = userDto.Email,
            Source = userDto.Source,
            // CreatedAt = DateTime.Now,
            // UpdatedAt = DateTime.Now,
        };
    }
    
    public static User ToUserModel(this CreateUserDTO userDto)
    {
        return new User()
        {
            Name = userDto.Name,
            Email = userDto.Email,
            Source = userDto.Source,
            // CreatedAt = DateTime.Now,
            // UpdatedAt = DateTime.Now,
        };
    }
}