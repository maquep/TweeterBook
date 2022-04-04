namespace TweeterBook.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string version = "v1";
        public const string Base = Root + "/" + version;
        public static class Posts
        {
            public const string GetAll = Base + "/posts";
            public const string Create = Base + "/posts";
            public const string Get = Base + " /posts/{postId}";
            public const string Update = Base + "/posts/{postId}";
            public const string Delete = Base + "/posts/{postId}";
        }

        public static class CosmosPosts
        {
            public const string GetAll = Base + "/cosmosposts";
            public const string Create = Base + "/cosmosposts";
            public const string Get = Base + " /cosmosposts/{postId}";
            public const string Update = Base + "/cosmosposts/{postId}";
            public const string Delete = Base + "/cosmosposts/{postId}";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
            public const string Refresh = Base + "/identity/refesh";
            public const string Delete = Base + "/identity/delete/{emailAddress}";
        }

        public static class Tags
        {
            public const string GetAll = Base + "/tags";
            public const string Create = Base + "/tags";
            public const string Get = Base + " /tags/{tagId}";
            public const string Update = Base + "/tags/{tagId}";
            public const string Delete = Base + "/tags/{tagId}";
        }
    }
}
