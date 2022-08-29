namespace UniquomeApp.WebApi.Models;

public class EmailSettings
{
    public bool UseRelay { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string SimpleSender { get; set; }
    public string SimpleSmtpServer { get; set; }
    public int SimpleSmtpPort { get; set; }
    public bool SimpleUseSsl { get; set; }
    public bool SendEmail { get; set; }
    public string RelaySender { get; set; }
    public string RelaySmtpServer { get; set; }
    public int RelaySmtpPort { get; set; }
    public bool RelayUseSsl { get; set; }
}