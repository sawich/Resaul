namespace Resaul.App.Domain.Requests;

public sealed record UserRegisterRequest()
{
    public string Username { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Region { get; init; } = string.Empty;
    public string UserLanguage { get; init; } = string.Empty;
    public string UserDateBirth { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string UserGender { get; init; } = string.Empty;
}
