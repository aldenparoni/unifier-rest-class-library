using UnifierWebServicesLibrary;
using ConsoleAppLibrary;

namespace TestConsoleApp
{
    internal class Program
    {
        static void Main() 
        {
            // Initialize program variables and objects
            bool continueLoop = true;
            int userNav;
            IntegrationUser user = ConsoleAppFunctions.GetToken();

            if (user.Token != string.Empty)
            {
                while (continueLoop == true)
                {
                    // Print menu
                    ConsoleAppFunctions.Menu();

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
                        Console.WriteLine($"\n{ex.Message}");
                    }
                }
            }

            Console.WriteLine("\nThank you for using this program!");
        }
    }
}