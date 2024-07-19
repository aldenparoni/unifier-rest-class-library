using UnifierWebServicesLibrary;
using BusinessProcessLibrary;
using Newtonsoft.Json;

namespace TestConsoleApp
{
    internal class Program
    {
        static void Main() 
        {
            bool continueLoop = true;
            int userNav;

            Console.Write("Enter 0 for Production, or 1 for Stage: ");
            int userEnv = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter your username: ");
            string? username = Console.ReadLine();
            Console.Write("Enter your password: ");
            string? password = UnifierRequests.ReadPassword();

            IntegrationUser user = new(userEnv, username, password);

            Console.WriteLine("\nWelcome to Unifier Web Services!");

            while (continueLoop == true)
            {
                Console.WriteLine("\nUnifier Web Services Menu:");
                Console.WriteLine("   1: Get a Business Process record");
                Console.WriteLine("   2: Create a new Business Process record");
                Console.WriteLine("   3: Update an existing Business Process record");
                Console.WriteLine("   4: Exit program");
                Console.Write("Please enter a number between 1 - 4: ");

                try
                {
                    userNav = Convert.ToInt32(Console.ReadLine());

                    if (userNav == 1)
                    {
                        Console.WriteLine("\nYou have selected 1: Get a Business Process record");
                        Console.Write("Enter the project number: ");
                        string? projectNum = Console.ReadLine();
                        Console.Write("Enter the name of the business process (type carefully): ");
                        string? bpName = Console.ReadLine();
                        Console.Write("Enter the record number: ");
                        string? recordNum = Console.ReadLine();
                        GetRecordInput input = new(bpName, recordNum);
                        UnifierRequests.GetBPRecord(user, projectNum, input);
                    }
                    else if (userNav == 2)
                    {
                        Console.WriteLine("\nYou have selected 2: Create a new Business Process record");
                        Console.Write("Enter the project number: ");
                        string? projectNum = Console.ReadLine();
                        Console.Write("Enter the name of the business process (type carefully): ");
                        string? bpName = Console.ReadLine();
                        if (projectNum != null && bpName == "ESI")
                        {
                            // JSONBody<Options, List<ESI>> json = new JSONBody<Options, List<ESI>>(new Options(projectNum, bpName), new ESI());
                            // string? body = JsonConvert.SerializeObject(json);
                        }
                    }
                    else if (userNav == 3)
                    {
                        Console.WriteLine("\nYou have selected 3: Update an existing Business Process record");
                    }
                    else if (userNav == 4)
                    {
                        Console.WriteLine("\nYou have selected 4: Exit program");
                        continueLoop = false;
                    }
                    else
                    {
                        Console.WriteLine($"\n{userNav} is not within range. Please enter a number within the range.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nPlease enter an integer.");
                }
            }

            Console.WriteLine("\nThank you for using this program!");


        }
    }
}