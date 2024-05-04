// See https://aka.ms/new-console-template for more information

using System.Text;
using Calabonga.ActiveDirectory;
using DotNetEnv;

Console.OutputEncoding = Encoding.UTF8;

Env.Load(".env", LoadOptions.TraversePath());

var rootSearch = Environment.GetEnvironmentVariable("LDAP_BASE_SEARCH") ?? throw new ArgumentNullException("LDAP_BASE_SEARCH");

// user entered data should be validated!

Console.WriteLine("Enter your login:");
var username = Console.ReadLine();

Console.WriteLine("Enter your password:");
var password = Console.ReadLine();

Console.WriteLine("Enter your DOMAIN (exp. \"DOMAINNAME\"):");
var domain = Console.ReadLine();
if (string.IsNullOrEmpty(domain))
{
	domain = Environment.GetEnvironmentVariable("LDAP_DOMAIN") ?? throw new ArgumentNullException("LDAP_DOMAIN");
}

Console.WriteLine("Enter your port (default 389):");
var port = Console.ReadLine();
if (string.IsNullOrEmpty(port))
{
	port = Environment.GetEnvironmentVariable("LDAP_PORT") ?? "389";
}

Console.WriteLine("Enter your ActiveDirectory IP or DNS-name (exp. \"domain.com\"):");
var server = Console.ReadLine();
if (string.IsNullOrEmpty(server))
{
	server = Environment.GetEnvironmentVariable("LDAP_SERVER") ?? throw new ArgumentNullException("LDAP_SERVER");
}

var options = new LdapConnectorOptions
{
	BaseSearch = rootSearch,
	Domain = domain,
	Port = int.Parse(port),
	Server = server
};

var result = LdapConnector.GetUserFromActiveDirectory(options, username, password);

if (result.Ok)
{
	foreach (var userGroup in result.User.Groups)
	{
		Console.WriteLine(userGroup);
	}
}