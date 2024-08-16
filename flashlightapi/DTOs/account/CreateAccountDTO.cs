using System.ComponentModel.DataAnnotations;

namespace flashlightapi.DTOs;

public class CreateAccountDTO
{
    [Required, MinLength(6)]
    public string Username {get; set;}

    [Required, EmailAddress] 
    public string Email {get; set;}

    [Required, MinLength(6)]
    public string Password {get; set; }
}