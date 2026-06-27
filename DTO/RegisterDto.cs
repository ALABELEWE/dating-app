using System.ComponentModel.DataAnnotations;


namespace DatingApp.DTO;

public class RegisterDto
{
    [Required]         
    public string DisplayName { get; set; } = "";

    [Required]          
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required]          
    [MinLength(6)]      
    public string Password { get; set; } = "";
}