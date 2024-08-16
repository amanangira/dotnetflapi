using flashlightapi.Models;

namespace flashlightapi.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}