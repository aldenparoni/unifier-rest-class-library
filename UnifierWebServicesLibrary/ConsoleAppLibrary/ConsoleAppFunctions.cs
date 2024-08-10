using BusinessProcessLibrary;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using UnifierWebServicesLibrary;

namespace ConsoleAppLibrary
{
    public class ConsoleAppFunctions
    {
        /// <summary>
        /// This method takes user input to create a new IntegrationUser object.
        /// </summary>
        /// <returns>A new IntegrationUser object that will be used throughout app runtime.</returns>
        public static IntegrationUser GetToken()
        {
            Console.WriteLine("Welcome to Unifier Web Services!");

            // Gather user input
            Console.Write("\nEnter 0 for Production, or 1 for Stage: ");
            int userEnv = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter your username: $$");
            string? username = "$$" + Console.ReadLine();
            Console.Write("Enter your password: ");
            string? password = UnifierRequests.ReadPassword();

            // Create new IntegrationUser instance
            return new IntegrationUser(userEnv, username, password);
        }

        /// <summary>
        /// This method prints the menu items of the console app program.
        /// </summary>
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
        /// This method takes user input and sets up the REST request of getting an existing Business Process record.
        /// </summary>
        /// <param name="user">The IntegrationUser object instance used throughout app runtime.</param>
        public static void GetRecordApp(IntegrationUser user)
        {
            // Gather user input
            Console.Write("\nEnter the project number: ");
            string? projectNum = Console.ReadLine();
            Console.Write("Enter the name of the business process (type carefully): ");
            string? bpName = Console.ReadLine();
            Console.Write("Enter the record number: ");
            string? recordNum = Console.ReadLine(); 

            // Create serialized JSON input parameter
            GetRecordInput input = new(bpName, recordNum);
            string inputJSON = JsonConvert.SerializeObject(input);

            // Execute the REST request
            Console.WriteLine($"\nGetting record number {recordNum} of {bpName} in {projectNum}...\n");
            Console.WriteLine(UnifierRequests.GetBPRecord(user, projectNum, inputJSON)); 
        }

        /// <summary>
        /// This method serves as the menu of the create BP record option.
        /// </summary>
        /// <param name="user">The IntegrationUser object instance used throughout app runtime.</param>
        public static void CreateRecordApp(IntegrationUser user)
        {
            // Declare user input variable for this function
            int? bpChoice;

            // Create BP Record menu
            Console.WriteLine("\nIn this console application, we have two business processes you can create records for: ");
            Console.WriteLine("  1. Engineer's Supplemental Instructions (ESI)");
            Console.WriteLine("  2. Canvassing Efforts");
            Console.Write("\nPlease enter 1 or 2 to pick from the above business processes: ");

            try
            {
                // Get user input for menu navigation
                bpChoice = Convert.ToInt32(Console.ReadLine());

                if (bpChoice == 1)
                {
                    Console.WriteLine($"\nYou have chosen {bpChoice}. Engineer's Supplemental Instructions (ESI)");
                    EngineersSupplementalInstructions.CreateESI(user);
                }
                else if (bpChoice == 2)
                {
                    Console.WriteLine($"\nYou have chosen {bpChoice}. Canvassing Efforts");
                    CanvassingEfforts.CreateEffort(user);
                }
                else
                {
                    // Returns to main menu
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

        /// <summary>
        /// This method serves as the menu of the update BP record option.
        /// </summary>
        /// <param name="user"></param>
        public static void UpdateRecordApp(IntegrationUser user)
        {
            // Declare user input variable for this function
            int? bpChoice;

            Console.WriteLine("\nThe steps this program will take to update a record: ");
            Console.WriteLine("  1. Get a BP record");
            Console.WriteLine("  2. Update any fields");
            Console.WriteLine("  3. Send update request");
            Console.WriteLine("  4. Receive results");

            Console.WriteLine("\nIn this console application, we have two business processes you can update records for: ");
            Console.WriteLine("  1. Engineer's Supplemental Instructions (ESI)");
            Console.WriteLine("  2. Canvassing Efforts");
            Console.Write("\nPlease enter 1 or 2 to pick from the above business processes: ");

            try
            {
                bpChoice = Convert.ToInt32(Console.ReadLine());
                if (bpChoice == 1)
                {
                    Console.WriteLine($"\nYou have chosen {bpChoice}. Engineer's Supplemental Instructions (ESI)");
                    string record = GetRecordToUpdateApp(user, "Engineer's Supplemental Instructions (ESI)");
                    if (record == string.Empty)
                    {
                        Console.WriteLine("\nSorry, the record was not found. Returning to main menu...");
                        return;
                    }
                    Console.WriteLine("\nRecord found!");
                    ReturnJSON<List<EngineersSupplementalInstructions>, List<string>, int> returnJSON =
                        JsonConvert.DeserializeObject<ReturnJSON<List<EngineersSupplementalInstructions>, List<string>, int>>(record);
                    Console.WriteLine(returnJSON.Data[0]);
                }
                else if (bpChoice == 2)
                {
                    Console.WriteLine($"\nYou have chosen {bpChoice}. Canvassing Efforts");
                    string record = GetRecordToUpdateApp(user, "Canvassing Efforts");
                    if (record == string.Empty)
                    {
                        Console.WriteLine("\nSorry, the record was not found. Returning to main menu...");
                        return;
                    }
                    Console.WriteLine("\nRecord found!");
                    ReturnJSON<List<CanvassingEfforts>, List<string>, int> returnJSON =
                        JsonConvert.DeserializeObject<ReturnJSON<List<CanvassingEfforts>, List<string>, int>>(record);
                    Console.WriteLine($"Name: {returnJSON.Data[0].Name}");
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="bpName"></param>
        /// <returns></returns>
        public static string GetRecordToUpdateApp(IntegrationUser user, string bpName)
        {
            Console.WriteLine("\nWe will get the record you want to update.");
            Console.Write("\nEnter the project number: ");
            string? projectNum = Console.ReadLine();
            Console.Write("Enter the record number: ");
            string? recordNum = Console.ReadLine();
            GetRecordInput input = new(bpName, recordNum);
            string inputJSON = JsonConvert.SerializeObject(input);
            Console.WriteLine($"\nGetting record number {recordNum} of {bpName} in {projectNum}...");
            string record = UnifierRequests.GetBPRecord(user, projectNum, inputJSON);
            
            return record ?? string.Empty;
        }
    }
}
