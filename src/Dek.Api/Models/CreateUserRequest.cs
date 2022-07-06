using System.ComponentModel.DataAnnotations;

namespace Dek.Api.Models;

public class CreateUserRequest
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
    public string? Email { get; set; }
}