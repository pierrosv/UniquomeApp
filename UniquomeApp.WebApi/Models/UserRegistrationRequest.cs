using System.ComponentModel.DataAnnotations;

namespace UniquomeApp.WebApi.Models
{
    public class UserRegistrationRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
    }
}