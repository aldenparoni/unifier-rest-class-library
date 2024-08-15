using UnifierWebServicesLibrary;
using ConsoleAppLibrary;

namespace TestConsoleApp
{
    internal class ConsoleApp
    {
        static void Main() 
        {
            // Initialize program variables and objects
            bool continueLoop = true;
            int userNav;

            Console.WriteLine("Welcome to Unifier Web Services!");

            // Gather user input to prepare HTTP request to retrieve auth token
            Console.Write("\nEnter 0 for Production, or 1 for Stage: ");
            int userEnv = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter your username: $$");
            string? username = "$$" + Console.ReadLine();
            Console.Write("Enter your password: ");
            string? password = UnifierRequests.ReadPassword();

            IntegrationUser user = new (userEnv, username, password);

            if (user.Token != string.Empty)
            {
                while (continueLoop == true)
                {
                    // Print menu
                    ConsoleAppFunctions.Menu(userEnv);

                    try
                    {
                        userNav = Convert.ToInt32(Console.ReadLine());

                        if (userNav == 1)
                        {
                            Console.WriteLine("\nYou have selected 1: Get a Business Process record");
                            ConsoleAppFunctions.GetRecordApp(user);
                        }
                        else if (userNav == 2)
                        {
                            Console.WriteLine("\nYou have selected 2: Create a new Business Process record");
                            ConsoleAppFunctions.CreateRecordApp(user);
                        }
                        else if (userNav == 3)
                        {
                            Console.WriteLine("\nYou have selected 3: Update an existing Business Process record");
                            ConsoleAppFunctions.UpdateRecordApp(user);
                        }
                        else if (userNav == 4)
                        {
                            Console.WriteLine("\nYou have selected 4: Switch environments");
                            (userEnv, user) = ConsoleAppFunctions.SwitchEnvironment(userEnv, username, password);
                        }
                        else if (userNav == 5)
                        {
                            Console.WriteLine("\nYou have selected 5: Exit program");
                            continueLoop = false;
                        }
                        else
                        {
                            Console.WriteLine($"\n{userNav} is not within range. Please enter a number within the range.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\n{ex.Message}");
                    }
                }
            }

            Console.WriteLine("\nThank you for using this program!");
        }
    }
}