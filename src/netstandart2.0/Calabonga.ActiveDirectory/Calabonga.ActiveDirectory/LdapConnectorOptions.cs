using System.Collections.Generic;

namespace Calabonga.ActiveDirectory
{
    /// <summary>
    /// LDAP connector settings for establishing connection with Active Directory
    /// </summary>
    public sealed class LdapConnectorOptions
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

        /// <summary>
        /// Parameters will fetch from Active Directory
        /// </summary>
        public List<ActiveDirectoryInfo> Attributes { get; set; }

        /// <summary>
        /// Allow to connect to LDAP with self-signed certificates
        /// </summary>
        public bool TrustAllCertificates { get; set; }
    }
}