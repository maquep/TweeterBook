namespace TweeterBook.Domain
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public string Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
