using flashlightapi.DTOs;
using flashlightapi.Interfaces;
using flashlightapi.Models;
using flashlightapi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace flashlightapi.Controllers;


[ApiController]
[Route("/api/account")]
public class AccountController(UserManager<AppUser> manager, ITokenService tokenService, SignInManager<AppUser> signInManager) : ControllerBase
{
    private UserManager<AppUser> _manager = manager;
    private ITokenService _tokenService = tokenService;
    private SignInManager<AppUser> _signInManager = signInManager;
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateAccountDTO createAccountDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var appUser = new AppUser()
            {
                UserName = createAccountDto.Username,
                Email = createAccountDto.Email,
            };

            var createdUser = await _manager.CreateAsync(appUser, createAccountDto.Password);

            if (!createdUser.Succeeded)
            {
                return StatusCode(500, createdUser.Errors);
            }

            var response = await _manager.AddToRoleAsync(appUser, "User");
            
            return !response.Succeeded ? StatusCode(500, response.Errors) : Ok(_tokenService.CreateToken(appUser));
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var user = await _manager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Username.ToLower());

        if (user == null)
        {
            return Unauthorized("invalid username");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        return result.Succeeded
            ? Ok(new AccountDTO
            {
                Username = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
            })
            : Unauthorized("invalid credentials");
    }
}