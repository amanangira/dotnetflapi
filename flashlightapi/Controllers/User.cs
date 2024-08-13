using flashlightapi.Data;
using flashlightapi.DTOs;
using flashlightapi.Interfaces;
using flashlightapi.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace flashlightapi.Controllers;

[Route("/api/user")]
[ApiController]

public class User(IUserRepository userRepository) : ControllerBase
{
    private readonly IUserRepository _userRepository = userRepository;

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var dbUsers = await _userRepository.ListAsync();
        var userDtos = dbUsers.Select(s => s.ToUserDto());

        return Ok(userDtos);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> Get([FromRoute] string userId)
    {
        var dbUser = await _userRepository.GetByIdAsync(Guid.Parse(userId));
        
        if (dbUser == null)
        {
            return NotFound();
        }

        return Ok(dbUser.ToUserDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDTO userDto)
    {
        var userModel = userDto.ToUserModel();
        await _userRepository.CreateAsync(userModel);


        return CreatedAtAction(nameof(Get), new { userId = userModel.Id }, userModel.ToUserDto());
    }

    [HttpPatch("{userId}")]
    public async Task<IActionResult> Update([FromBody] UserDTO userDto, string userId)
    {
        await _userRepository.UpdateAsync(Guid.Parse(userId), userDto.ToUserModel());

        return Ok();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> Delete(string userId)
    {
        await _userRepository.DeleteAsync(Guid.Parse(userId));

        return NoContent();
    }
    
    
}