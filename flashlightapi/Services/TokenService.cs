using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using flashlightapi.Interfaces;
using flashlightapi.Models;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace flashlightapi.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config; 
    private readonly SymmetricSecurityKey _key;
    
    public TokenService(IConfiguration config)
    {
        _config = config; 
        // TODO - throw exception if configuration is empty
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"])); 
    }
    
    public string CreateToken(AppUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Expires    = DateTime.Now.AddDays(7),
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = creds,
            Issuer = _config["JWT:Issuer"],
            Audience = _config["JWT:Audience"],
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}