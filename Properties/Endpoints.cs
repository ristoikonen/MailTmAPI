namespace MailTmAPI.Properties
{
    internal static class Endpoints
    {
        public static readonly Uri ApiRoot = new Uri("https://api.mail.tm");
        public const string Host = "api.mail.tm";

        public const string Accounts = "accounts";
        public const string Account = "accounts/{0}";
        public const string Me = "me";

        public const string Domains = "domains";
        public const string Domain = "domains/{0}";

        public const string Messages = "messages";
        public const string MessagesByPage = "messages?page={0}";
        public const string Message = "messages/{0}";

        public const string Sources = "sources/{0}";

        public const string Token = "token";
    }
}
