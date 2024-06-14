using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PKS4.Models.Data;

[Table("User", Schema = "public")]
public class User
{
    [Key]
    [Column("user_id")]
    public long Id { get; set; }

    [Required]
    [Column("user_login")]
    public required string Login { get; set; }

    [Required]
    [Column("user_password")]
    public required string Password { get; set; }

    [Column("user_name")]
    public string Name { get; set; } = string.Empty;
    [Column("user_surname")]
    public string Surname { get; set; } = string.Empty;
    [Column("user_middlename")]
    public string MiddleName { get; set; } = string.Empty;

    public User() { }

    public User(string login,  string password)
    {
        Login = login;
        Password = password;
    }
}
