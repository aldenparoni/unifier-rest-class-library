using BusinessProcessLibrary;
using UnifierWebServicesLibrary;

namespace ConsoleAppLibrary
{
    public class ConsoleAppFunctions
    {
        public static void GetToken()
        {
            Console.WriteLine("Welcome to Unifier Web Services!");
            Console.Write("Enter 0 for Production, or 1 for Stage: ");
            int userEnv = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter your username: $$");
            string? username = "$$" + Console.ReadLine();
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
            Console.WriteLine($"\nGetting record number {recordNum} of {bpName} in {projectNum}...\n");
            Console.WriteLine(UnifierRequests.GetBPRecord(user, projectNum, input)); 
        }

        public static void CreateRecordApp(IntegrationUser user)
        {
            Console.WriteLine("In this console application, we have two business processes you can create records for: ");
            Console.WriteLine("  1. Engineer's Supplemental Instructions (ESI)");
            Console.WriteLine("  2. Canvassing Efforts");
            Console.Write("\nPlease enter 1 or 2 to pick from the above business processes: ");
        }
    }
}
