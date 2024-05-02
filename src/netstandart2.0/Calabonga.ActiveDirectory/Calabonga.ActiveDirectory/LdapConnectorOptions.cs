namespace Calabonga.ActiveDirectory
{
    public class LdapConnectorOptions
    {
        public string Server { get; set; }

        public int Port { get; set; }

        /// <summary>
        /// Domain name
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Timeout is seconds 
        /// </summary>
        public int Timeout { get; set; } = 30;

        /// <summary>
        /// Root search as base point to start object find.<br/>
        /// For example, <example>"DC=domain,DC=com"</example>
        /// </summary>
        public string BaseSearch { get; set; }
    }
}