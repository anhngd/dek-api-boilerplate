namespace Dek.Api.Entities;

public class User : BaseEntity
{
    public User(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = password;
    }

    private string Username { get; set; }
    private string Email { get; set; }
    private string Password { get; set; }
    
    
}