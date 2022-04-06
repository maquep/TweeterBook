//using System.ComponentModel.DataAnnotations;

namespace TweeterBook.Contracts.V1.Requests
{
    public class UserRegistrationRequest
    {
        public string Name { get; set; }

        //[EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
