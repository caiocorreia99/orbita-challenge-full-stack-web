namespace Student_Portal_API.Helpers
{
    public class Constants
    {
        /* Route Specification */
        public const string ApiVersionPrefix = "v{v:apiVersion}";
        public const string AuthenticationRoute = $"/api/{ApiVersionPrefix}/core/login";
        public const string UserRoute = $"/api/{ApiVersionPrefix}/core/user";
        public const string StudentRoute = $"/api/{ApiVersionPrefix}/core/student";

    }
}
