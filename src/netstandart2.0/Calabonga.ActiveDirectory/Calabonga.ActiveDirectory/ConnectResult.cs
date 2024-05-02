namespace Calabonga.ActiveDirectory
{
    public class ConnectResult
    {
        public ConnectResult(DirectoryUser user)
        {
            User = user;
        }

        public ConnectResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public DirectoryUser User { get; }

        public string ErrorMessage { get; }

        public bool Ok => ErrorMessage == null && User != null;
    }
}