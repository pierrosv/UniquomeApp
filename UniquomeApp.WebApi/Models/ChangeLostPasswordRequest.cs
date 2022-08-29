namespace UniquomeApp.WebApi.Models;

public class ChangeLostPasswordRequest
{
    public string Token { get; set; }
    public string NewPassword { get; set; }
}