namespace Calabonga.ActiveDirectory
{
    /// <summary>
    /// Information from Active Directory attribute
    /// </summary>
    public class ActiveDirectoryInfo
    {
        /// <summary>
        /// Key to search
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Processing type after attributes fetch
        /// </summary>
        public DirectoryAttributeType Type { get; set; }

        /// <summary>
        /// Processed value
        /// </summary>
        public object Value { get; set; }
    }
}