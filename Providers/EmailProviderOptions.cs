namespace Providers;

public class EmailProviderOptions
{
    public string DisplayName { get; set; } = string.Empty;
    public string MailAddress { get; set; } = string.Empty;
    public string SmtpHost { get; set; } = string.Empty;
    public int SmtpPort { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool EnableSsl { get; set; }
}