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

        public static void GetRecordApp(IntegrationUser user)
        {
            Console.Write("Enter the project number: ");
            string? projectNum = Console.ReadLine();
            Console.Write("Enter the name of the business process (type carefully): ");
            string? bpName = Console.ReadLine();
            Console.Write("Enter the record number: ");
            string? recordNum = Console.ReadLine(); 
            GetRecordInput input = new(bpName, recordNum);
            UnifierRequests.GetBPRecord(user, projectNum, input);
        }
    }
}
