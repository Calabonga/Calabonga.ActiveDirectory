// See https://aka.ms/new-console-template for more information

using System.Text;
using Calabonga.ActiveDirectory;

Console.OutputEncoding = Encoding.UTF8;

// user entered data should be validated!

Console.WriteLine("Enter your login:");
var username = Console.ReadLine();

Console.WriteLine("Enter your password:");
var password = Console.ReadLine();

Console.WriteLine("Enter your DOMAIN:");
var domain = Console.ReadLine();

Console.WriteLine("Enter your port:");
var port = Console.ReadLine();

Console.WriteLine("Enter your ActiveDirectory IP or DNS-name:");
var server = Console.ReadLine();

var options = new LdapConnectorOptions
{
    BaseSearch = "DC=msto,DC=lo",
    Domain = domain,
    Port = int.Parse(port!),
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