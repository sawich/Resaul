namespace Resaul.App.Domain.Requests;

public sealed record UserLoginRequest
{
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}
