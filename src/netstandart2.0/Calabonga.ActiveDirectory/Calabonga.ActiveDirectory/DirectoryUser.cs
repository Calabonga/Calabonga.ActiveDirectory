using System;
using System.Collections.Generic;
using System.Linq;

namespace Calabonga.ActiveDirectory
{
    /// <summary>
    /// User found in Active Directory
    /// </summary>
    public class DirectoryUser
    {
        private DirectoryUser(string username)
        {
            Username = username;
        }

        public string Username { get; }

        public List<string> Groups { get; private set; } = new List<string>();

        public static DirectoryUser Create(LdapConnectorOptions ldapOptions, string username)
        {
            var user = new DirectoryUser(username)
            {
                Attributes = ldapOptions.Attributes
            };
            return user;
        }

        public List<ActiveDirectoryInfo> Attributes { get; private set; }

        public T GetValueFromAttributes<T>(string key)
        {
            try
            {
                if (key is null || Attributes is null || !Attributes.Any())
                {
                    return Activator.CreateInstance<T>();
                }

                var item = Attributes.FirstOrDefault(x => x.Key == key);

                return (T)item?.Value;
            }
            catch
            {
                throw;
            }
        }
    }
}