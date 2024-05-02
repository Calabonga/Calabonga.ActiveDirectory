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
        public static ConnectResult GetUserFromActiveDirectory(LdapConnectorOptions ldapOptions, string username, string password)
        {
            try
            {
                using (var cn = new LdapConnection())
                {
                    const string searchFilter = "(&(objectClass=user)(sAMAccountName={0}))";
                    cn.Connect(ldapOptions.Server, ldapOptions.Port);
                    cn.Timeout = new TimeSpan(0, 0, ldapOptions.Timeout);
                    cn.Bind(Native.LdapAuthType.Simple, new LdapCredential { UserName = $"{ldapOptions.Domain}\\{username}", Password = password });

                    var entries = cn.Search(ldapOptions.BaseSearch, string.Format(searchFilter, username), scope: Native.LdapSearchScope.LDAP_SCOPE_SUBTREE);
                    if (!entries.Any())
                    {
                        return new ConnectResult(new DirectoryUser(username));
                    }

                    var user = new DirectoryUser(username);

                    foreach (var element in entries)
                    {
                        var directoryAttributes = element.DirectoryAttributes["memberof"];
                        var values = directoryAttributes.GetValues<string>();
                        foreach (var value in values)
                        {
                            var index = value.IndexOf(',');
                            var group = value.Substring(0, index).Split('=')[1];
                            user.Groups.Add(group);
                        }
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
