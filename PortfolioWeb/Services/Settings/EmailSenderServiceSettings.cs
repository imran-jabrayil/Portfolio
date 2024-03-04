namespace PortfolioWeb.Services.Settings;

public record EmailSenderServiceSettings {
    public string Host { get; init; } = null!;
    public int Port { get; init; }
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string NoReplyEmail { get; init; } = null!;
}