using Microsoft.AspNetCore.Identity;

namespace Resaul.App.Domain.Models;

public class UserModel : IdentityUser
{
    public string Region { get; set; } = string.Empty;
    public string UserLanguage { get; set; } = string.Empty;
    public string UserDateBirth { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string UserGender { get; set; } = string.Empty;
}
