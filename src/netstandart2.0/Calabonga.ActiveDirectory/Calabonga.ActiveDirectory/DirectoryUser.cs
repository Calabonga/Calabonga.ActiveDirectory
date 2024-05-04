using System;
using System.Collections.Generic;
using System.Linq;

namespace Calabonga.ActiveDirectory
{
	/// <summary>
	/// User found in Active Directory
	/// </summary>
	public sealed class DirectoryUser
	{
		private DirectoryUser(string username)
		{
			Username = username;
		}

		/// <summary>
		/// User name
		/// </summary>
		public string Username { get; }

		/// <summary>
		/// Active Directory group where user in.
		/// </summary>
		public List<string> Groups { get; private set; } = new List<string>();

		public static DirectoryUser Create(LdapConnectorOptions ldapOptions, string username)
		{
			var user = new DirectoryUser(username)
			{
				Attributes = ldapOptions.Attributes
			};
			return user;
		}

		/// <summary>
		/// The list of attributes for search in Active Directory. It should be predefined before make any search requests.
		/// </summary>
		public List<ActiveDirectoryInfo> Attributes { get; private set; }

		/// <summary>
		/// Searches values in <see cref="Attributes"/>. If attribute found and value filled you can get it using this method.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <returns></returns>
		public T GetValueFromAttributes<T>(string key)
		{
			try
			{
				if (key is null || Attributes is null || !Attributes.Any())
				{
					return Activator.CreateInstance<T>();
				}

				var item = Attributes.FirstOrDefault(x => x.Key == key);

				var value = (T)item?.Value;
				return value;
			}
			catch
			{
				throw;
			}
		}
	}
}