namespace TweeterBook.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string version = "v1";
        public const string Base = Root + "/" + version;
        public static class Posts
        {
            public const string GetAll = Base + " /posts";
            public const string Create = Base + " /posts";
            public const string Get = Base + " /posts/{postId}";
            public const string Update = Base + " /posts/{postId}";
        }
    }
}
