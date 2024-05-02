using System.Collections.Generic;

namespace Calabonga.ActiveDirectory
{
    public class DirectoryUser
    {
        public DirectoryUser(string username)
        {
            Username = username;
        }

        public string Username { get; }

        public List<string> Groups { get; set; } = new List<string>();
    }
}