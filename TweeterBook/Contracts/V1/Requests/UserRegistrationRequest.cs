﻿namespace TweeterBook.Contracts.V1.Requests
{
    public class UserRegistrationRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
