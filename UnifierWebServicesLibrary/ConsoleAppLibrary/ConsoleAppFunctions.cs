using BusinessProcessLibrary;
using Newtonsoft.Json;
using UnifierWebServicesLibrary;

namespace ConsoleAppLibrary
{
    public class ConsoleAppFunctions
    {
        /// <summary>
        /// This method takes user input to create a new IntegrationUser object, which includes generating a new auth token.
        /// </summary>
        /// <returns>A new IntegrationUser object that will be used throughout app runtime.</returns>
        public static IntegrationUser GetToken()
        {
            Console.WriteLine("Welcome to Unifier Web Services!");
            Console.Write("\nEnter 0 for Production, or 1 for Stage: ");
            int userEnv = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter your username: $$");
            string? username = "$$" + Console.ReadLine();
            Console.Write("Enter your password: ");
            string? password = UnifierRequests.ReadPassword();

            return new IntegrationUser(userEnv, username, password);
        }

        public static void Menu()
        {
            Console.WriteLine("\nUnifier Web Services Menu:");
            Console.WriteLine("   1: Get a Business Process record");
            Console.WriteLine("   2: Create a new Business Process record");
            Console.WriteLine("   3: Update an existing Business Process record");
            Console.WriteLine("   4: Exit program");
            Console.Write("Please enter a number between 1 - 4: ");
        }

        /// <summary>
        /// This method prints the information of a record requested by user parameters.
        /// </summary>
        /// <param name="user">The IntegrationUser object instance used throughout app runtime.</param>
        public static void GetRecordApp(IntegrationUser user)
        {
            Console.Write("\nEnter the project number: ");
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
            // Declare user input variable for this function
            int? bpChoice = 0;

            Console.WriteLine("In this console application, we have two business processes you can create records for: ");
            Console.WriteLine("  1. Engineer's Supplemental Instructions (ESI)");
            Console.WriteLine("  2. Canvassing Efforts");
            Console.Write("\nPlease enter 1 or 2 to pick from the above business processes: ");
            try
            {
                bpChoice = Convert.ToInt32(Console.ReadLine());
                if (bpChoice == 1)
                {
                    Console.WriteLine($"\nYou have chosen {bpChoice}. Engineer's Supplemental Instructions (ESI)");
                }
                else if (bpChoice == 2)
                {
                    Console.WriteLine($"\nYou have chosen {bpChoice}. Canvassing Efforts");

                    Console.Write("\nEnter the project number: ");
                    string? projectNum = Console.ReadLine();
                    Options options = new Options(projectNum, "Canvassing Efforts");

                    Console.WriteLine("Please enter record information below.");
                    Console.Write("\n                                        Name: ");
                    string? effortName = Console.ReadLine();
                    Console.Write("\n  Canvassing Project (enter a record number): ");
                    string? canvasProject = Console.ReadLine();
                    Console.Write("\n            Start Date (MM-DD-YYYY HH:MM:SS): ");
                    string? startDate = Console.ReadLine();
                    Console.Write("\n              End Date (MM-DD-YYYY HH:MM:SS): ");
                    string? endDate = Console.ReadLine();
                    Console.Write("\n        All-Encompassing Effort? (yes or no): ");
                    string? allEncompassing = Console.ReadLine();
                    Console.Write("\n           Status (Active, Inactive, Voided): ");
                    string? status = Console.ReadLine();
                    Console.Write("\n                  Void (1 for yes, 0 for no): ");
                    string? isVoid = Console.ReadLine();

                    List<CanvassingEfforts> data = new List<CanvassingEfforts>
                    { 
                        new CanvassingEfforts(effortName, canvasProject, startDate, endDate, allEncompassing, status, isVoid)
                    };

                    JSONBody<Options, List<CanvassingEfforts>> jsonBody = new(options, data);
                    string body = JsonConvert.SerializeObject(jsonBody, Formatting.Indented);

                    Console.WriteLine($"\nThank you. Now creating record in Unifier...");

                    UnifierRequests.CreateBPRecord(user, body);
                }
                else
                {
                    Console.WriteLine($"\nYou entered {bpChoice}, which is not in range. Please enter a number in range.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex.Message}");
                Console.WriteLine("Redirecting back to main menu...");
                return;
            }
        }
    }
}
