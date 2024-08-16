using BusinessProcessLibrary;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using UnifierWebServicesLibrary;

namespace ConsoleAppLibrary
{
    public class ConsoleAppFunctions
    {
        /// <summary>
        /// This method prints the menu items of the console app program.
        /// </summary>
        /// <param name="environment"></param>
        public static void Menu(int environment)
        {
            if (environment == 0)
            {
                Console.WriteLine("\nYou are currently in production.");
            }
            else
            {
                Console.WriteLine("\nYou are currently in stage.");
            }
            Console.WriteLine("\nUnifier Web Services Menu:");
            Console.WriteLine("   1: Get a Business Process record");
            Console.WriteLine("   2: Create a new Business Process record");
            Console.WriteLine("   3: Update an existing Business Process record");
            Console.WriteLine("   4: Switch environments");
            Console.WriteLine("   5: Exit program");
            Console.Write("Please enter a number between 1 - 5: ");
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
            string responseContent = UnifierRequests.GetBPRecord(user, projectNum, inputJSON);

            if (responseContent != string.Empty)
            {
                // Deserialize the JSON-formatted string into a ReturnJSON object
                GetReturnJSON<object, List<string>, int> returnJSON =
                    JsonConvert.DeserializeObject<GetReturnJSON<object, List<string>, int>>(responseContent);

                if (returnJSON.Status == 200)
                {
                    Console.WriteLine($"Record found! Here's the full record information:\n");
                    Console.WriteLine(responseContent);
                }
                else
                {
                    Console.WriteLine($"There was an error in searching for the record: Status code {returnJSON.Status} ({returnJSON.Message[0]})");
                }

                Console.WriteLine("\nNow returning to main menu...");
                return;
            }

            Console.WriteLine("The HTTP request returned a null response. Now returning to main menu...");
        }

        /// <summary>
        /// This method serves as the menu of the create BP record option.
        /// It also takes user input and sets up the REST request of creating a new Business Process record.
        /// For the console app prototype, the user can create new ESI records or Canvassing Efforts records.
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
            Console.WriteLine("  3. Areas");
            Console.Write("\nPlease enter 1, 2, or 3 to pick from the above business processes: ");

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
                else if (bpChoice == 3)
                {
                    Console.WriteLine($"\nYou have chosen {bpChoice}. Areas");
                    Areas.CreateArea(user);
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
        /// It also takes user input and sets up the REST request of updating an existing Business Process record.
        /// For the console app prototype, the user can update existing ESI records or Canvassing Efforts records.
        /// </summary>
        /// <param name="user">The IntegrationUser object instance used throughout app runtime.</param>
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

                    var (projectNum, record) = GetRecordToUpdateApp(user, "Engineer's Supplemental Instructions (ESI)");

                    GetReturnJSON<List<EngineersSupplementalInstructions>, List<string>, int> initReturn =
                        JsonConvert.DeserializeObject<GetReturnJSON<List<EngineersSupplementalInstructions>, List<string>, int>>(record);

                    if (record == string.Empty || initReturn.Status != 200)
                    {
                        Console.WriteLine("\nSorry, the record was not found. Returning to main menu...");
                        return;
                    }

                    Console.WriteLine("\nRecord found!");

                    initReturn.Data[0].UpdateESI();

                    Console.WriteLine("\nHere's the updated record information:\n");
                    UnifierRequests.PrintRecordInfo(initReturn.Data[0]);

                    Console.WriteLine("\nNow sending the update request...\n");

                    // Set up the JSON body to send the update request
                    WorkflowDetails workflowDetails = new("Lead Review", "Update Status");
                    Options options = new(projectNum, "Engineer's Supplemental Instructions (ESI)", workflowDetails);
                    JSONBody<Options, List<EngineersSupplementalInstructions>> updateJSON = new(options, initReturn.Data);
                    string body = JsonConvert.SerializeObject(updateJSON);

                    string requestContent = UnifierRequests.UpdateBPRecord(user, body);

                    UnifierRequests.PostPutRequestCheck(2, requestContent);
                }
                else if (bpChoice == 2)
                {
                    Console.WriteLine($"\nYou have chosen {bpChoice}. Canvassing Efforts");

                    var (projectNum, record) = GetRecordToUpdateApp(user, "Canvassing Efforts");
 
                    GetReturnJSON<List<CanvassingEfforts>, List<string>, int> initReturn =
                        JsonConvert.DeserializeObject<GetReturnJSON<List<CanvassingEfforts>, List<string>, int>>(record);

                    if (record == string.Empty || initReturn.Status != 200)
                    {
                        Console.WriteLine("\nSorry, the record was not found. Returning to main menu...");
                        return;
                    }

                    Console.WriteLine("\nRecord found!");

                    initReturn.Data[0].UpdateEffort();

                    Console.WriteLine("\nHere's the updated record information:\n");
                    UnifierRequests.PrintRecordInfo(initReturn.Data[0]);

                    Console.WriteLine("\nNow sending the update request...\n");

                    // Set up the JSON body to send the update request
                    Options options = new (projectNum, "Canvassing Efforts");
                    JSONBody<Options, List<CanvassingEfforts>> updateJSON = new (options, initReturn.Data);
                    string body = JsonConvert.SerializeObject(updateJSON);

                    string requestContent = UnifierRequests.UpdateBPRecord(user, body);

                    UnifierRequests.PostPutRequestCheck(2, requestContent);
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
        /// This method is similar to GetRecordApp(), but is meant to be the first part of UpdateRecordApp(), which is
        /// to retrive the record and its information the user intends to update.
        /// </summary>
        /// <param name="user">The IntegrationUser object instance used throughout app runtime.</param>
        /// <param name="bpName">The name of the business process from which to the get the record.</param>
        /// <returns>
        /// The first string that the method returns is the project number.
        /// The second string that the method returns is either the JSON of the request results, or an empty string.
        /// </returns>
        public static (string, string) GetRecordToUpdateApp(IntegrationUser user, string bpName)
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
            
            if (record == null)
            {
                return (string.Empty, string.Empty);
            }

            return (projectNum, record);
        }

        /// <summary>
        /// This method switches the environment in Unifier the user currently works in during runtime,
        /// from production to stage, or vice versa.
        /// </summary>
        /// <param name="environment">An integer value that corresponds to the current envirornment.</param>
        /// <param name="username">The integration user's username ID in Unifier.</param>
        /// <param name="password">The integration user's password in Unifier.</param>
        /// <returns>
        /// The integer value that returns corresponds to the new environment the user will work in.
        /// The IntegrationUser object instance that returns contains the auth token of the new environment.
        /// </returns>
        public static (int, IntegrationUser) SwitchEnvironment(int environment, string username, string password)
        {
            // If current environment is production, switch to stage
            if (environment == 0)
            {
                environment = 1;
                return (environment, new IntegrationUser(environment, username, password));
            }

            // Vice versa; if current environment is stage, switch to production
            environment = 0;
            return (environment, new IntegrationUser(environment, username, password));
        }
    }
}
