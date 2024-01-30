namespace Resaul.App.Domain.Responses;

public sealed record UserResponse
{
    public string UserName { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string UserLanguage { get; set; } = string.Empty;
    public string UserDateBirth { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string UserGender { get; set; } = string.Empty;
}
