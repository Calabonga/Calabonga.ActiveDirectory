using System;
using System.Collections.Generic;
using System.Globalization;
using LdapForNet;

namespace Calabonga.ActiveDirectory
{
    /// <summary>
    /// Helper for read data from Active Directory attributes
    /// </summary>
    public static class DirectoryReader
    {
        /// <summary>
        /// Trying to get values by key and fill values from attributes
        /// </summary>
        public static void TryGetValues(SearchResultAttributeCollection items, List<ActiveDirectoryInfo> attributes)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (attributes is null)
            {
                throw new ArgumentNullException(nameof(attributes));
            }

            foreach (var item in attributes)
            {
                try
                {
                    if (item.Type == DirectoryAttributeType.Guid)
                    {
                        item.Value = new Guid((Byte[])(Array)items["objectGUID"].GetValue<byte[]>());
                    }

                    if (item.Type == DirectoryAttributeType.DateTime)
                    {
                        item.Value = DateTime.ParseExact(items[item.Key].GetValue<string>(), "yyyyMMddhhmmss.0Z", CultureInfo.InvariantCulture);
                    }

                    if (item.Type == DirectoryAttributeType.String)
                    {
                        item.Value = items[item.Key].GetValue<string>();
                    }

                    if (item.Type == DirectoryAttributeType.StringList)
                    {
                        item.Value = items[item.Key].GetValues<string>();
                    }

                    if (item.Type == DirectoryAttributeType.ByteArray)
                    {
                        item.Value = items[item.Key].GetValue<byte[]>();
                    }

                    if (item.Type == DirectoryAttributeType.ByteArrayList)
                    {
                        item.Value = items[item.Key].GetValues<byte[]>();
                    }
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}