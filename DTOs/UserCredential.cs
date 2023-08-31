using System.ComponentModel.DataAnnotations;

namespace CostMSWebAPI.DTOs;

public partial class UserCredential
{
    [Required(ErrorMessage = "User Name is required")]
    public required string Username { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public required string Password { get; set; }
}
