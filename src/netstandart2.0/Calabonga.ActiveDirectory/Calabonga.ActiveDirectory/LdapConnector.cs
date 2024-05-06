using System;
using System.Linq;
using LdapForNet;
using LdapForNet.Native;

namespace Calabonga.ActiveDirectory
{
    /// <summary>
    /// Connector allows to connect Active Directory using nuget <see cref="LdapForNet"/>
    /// </summary>
    public static class LdapConnector
    {
        /// <summary>
        /// Check user exists in Active Directory through the LDAP
        /// </summary>
        /// <param name="ldapOptions"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static ConnectResult GetUserFromActiveDirectory(LdapConnectorOptions ldapOptions, string username, string password)
        {
            try
            {
                using (var connection = new LdapConnection())
                {
                    connection.Connect(ldapOptions.Server, ldapOptions.Port);

                    if (ldapOptions.TrustAllCertificates)
                    {
                        connection.TrustAllCertificates();
                    }

                    connection.Timeout = new TimeSpan(0, 0, ldapOptions.Timeout);

                    connection.Bind(ldapOptions.LdapAuthType, new LdapCredential
                    {
                        UserName = $"{ldapOptions.Domain}\\{username}",
                        Password = password
                    });

                    var entries = connection.Search(
                        ldapOptions.BaseSearch,
                        $"(&(objectClass=user)(sAMAccountName={username}))",
                        scope: Native.LdapSearchScope.LDAP_SCOPE_SUBTREE);

                    if (!entries.Any())
                    {
                        return new ConnectResult(DirectoryUser.Create(ldapOptions, username));
                    }

                    var user = DirectoryUser.Create(ldapOptions, username);

                    if (entries.Any())
                    {
                        DirectoryReader.TryGetValues(entries[0].DirectoryAttributes, user.Attributes);
                    }

                    return new ConnectResult(user);
                }
            }
            catch (Exception exception)
            {
                return new ConnectResult(exception.Message);
            }
        }
    }
}
