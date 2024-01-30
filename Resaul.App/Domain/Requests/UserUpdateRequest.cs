namespace Resaul.App.Domain.Requests;

public sealed record UserUpdateRequest
{
    public long Id { get; init; }
    public string Username { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public int UserStatus { get; init; }
    public string Region { get; init; } = string.Empty;
    public string UserLanguage { get; init; } = string.Empty;
    public int UserAge { get; init; }
    public string UserGender { get; init; } = string.Empty;
    public DateOnly UserDateBirth { get; init; }
    public string DiscordToken { get; init; } = string.Empty;
    public string SteamToken { get; init; } = string.Empty;
    public string TwitchToken { get; init; } = string.Empty;
    public string GoogleToken { get; init; } = string.Empty;
}
