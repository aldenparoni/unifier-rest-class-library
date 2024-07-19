using BusinessProcessLibrary;
using UnifierWebServicesLibrary;

namespace ConsoleAppLibrary
{
    public class ConsoleAppFunctions
    {
        public static void App()
        {
            Console.WriteLine("Welcome to Unifier Web Services!");
            Console.Write("Enter 0 for Production, or 1 for Stage: ");
            int userEnv = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter your username: ");
            string? username = Console.ReadLine();
            Console.Write("Enter your password: ");
            string? password = UnifierRequests.ReadPassword();

            IntegrationUser user = new(userEnv, username, password);
        }
    }
}
