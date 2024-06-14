using System.ComponentModel.DataAnnotations;

namespace PKS4.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Username is required")]
    public required string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public required string Password { get; set; }
}
