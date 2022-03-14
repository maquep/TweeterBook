using System.Collections.Generic;

namespace TweeterBook.Domain
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public string ResponseToken { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
