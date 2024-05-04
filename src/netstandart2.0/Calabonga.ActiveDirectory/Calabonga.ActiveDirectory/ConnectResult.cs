namespace Calabonga.ActiveDirectory
{
	/// <summary>
	/// Connection operation result
	/// </summary>
	public sealed class ConnectResult
	{
		public ConnectResult(DirectoryUser user)
		{
			User = user;
		}

		public ConnectResult(string errorMessage)
		{
			ErrorMessage = errorMessage;
		}

		/// <summary>
		/// User found in Active Directory
		/// </summary>
		public DirectoryUser User { get; }

		/// <summary>
		/// Error message when any errors occured
		/// </summary>
		public string ErrorMessage { get; }

		/// <summary>
		/// Operation success indicator
		/// </summary>
		public bool Ok => ErrorMessage == null && User != null;
	}
}