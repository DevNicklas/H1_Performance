using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace Performance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StartController();
        }

        #region Model
        /// <summary>
        /// Gets pressed key from the user
        /// </summary>
        /// <returns>The key which the user pressed</returns>
        private static ConsoleKey GetPressedKey()
        {
            return Console.ReadKey(true).Key;
        }

        /// <summary>
        /// Gets user input by using readline
        /// </summary>
        /// <returns>User input as a string</returns>
        private static string GetUserInput()
        {
            return Console.ReadLine();
        }
        #endregion

        #region View
        /// <summary>
        /// Draws the first menu which contains all the assignments the program can do for the user
        /// </summary>
        private static void DrawMenu()
        {
            Console.Clear();
            Console.WriteLine("SECTION: Write only");
            Console.WriteLine("1. Make an text file with a amount of random integers you choose");
            Console.WriteLine("2. Add an integer of your choice to the database");
            Console.WriteLine("\nSECTION: Read only");
            Console.WriteLine("3. Search for a certain random integer in the database");
            Console.WriteLine("4. Get a sorted overview of all integers and how many times they occur");
            Console.WriteLine("5. Find the rarest random integer or integers that occur");
            Console.WriteLine("6. Find the most frequent random integer or integers that occur");
            Console.WriteLine("7. Find the rarest and most frequent integer");
            Console.WriteLine("\nPress the key that corresponds to what you want to do, or any other key to exit");
        }

        /// <summary>
        /// Draws the menu for create a text file with a amount of random integers of the users liking
        /// </summary>
        private static void DrawCreateFileMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("You've selected: \n(1) Make an text file with a amount of random integers you choose");

            Console.ResetColor();
            Console.WriteLine("\n-------------------------------------------\n");
            Console.WriteLine("Description:");
            Console.WriteLine("A text file will be created if it doesn't exist, and");
            Console.WriteLine("a chosen amount of randon integers will be written in it.");
            Console.WriteLine("\nRules:");
            Console.WriteLine("- The minimum amount of random integers you can choose is 1");
            Console.WriteLine("- The maximum amount of random integer you can choose is 10.000.000");
            Console.WriteLine("\n-------------------------------------------\n");
        }
        
        /// <summary>
        /// Draws a output which tells the user to give a user input
        /// </summary>
        /// <param name="text">output which tells the user to give a user input</param>
        private static void DrawGetUserInput(string text)
        {
            Console.WriteLine(text);
        }
        #endregion

        #region Controller
        /// <summary>
        /// The first controller which handles the program
        /// </summary>
        private static void StartController()
        {
            // Draws the first and the overall menu
            DrawMenu();

            // Checks which assignment the program should run
            ChooseAssignment();
        }

        /// <summary>
        /// The user can choose an assignment by pressing the key which corresponds to the assignment
        /// </summary>
        private static void ChooseAssignment()
        {
            switch (GetPressedKey())
            {
                case ConsoleKey.D1:
                    DrawCreateFileMenu();
                    CreateFile();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Creates a txt file and writes the given amount of random numbers the user chose
        /// </summary>
        private static void CreateFile()
        {
            string fileName = "RandomNumbers.txt";

            // Gets the user input and checks whether the file doesn't exist
            // the user input is an integer, the user input is minimum 1 and maximum 10.000.000
            int userInput = 0;
            DrawGetUserInput("Write how many random numbers the file should contain:");
            while (!int.TryParse(GetUserInput(), out userInput) || userInput < 1 || userInput > 10000000 || File.Exists(fileName))
            {
                DrawGetUserInput("Something went wrong, remember to read the rules\nWrite how many random numbers the file should contain:");
            }

            File.Create(fileName).Close();

            Random rand = new Random();

            // Writes every random number, with a 'id' into the created file
            // Starts at 1 and ends at the integer the user chose
            // Just like this: 1, 6923
            using (StreamWriter sw = File.AppendText(fileName))
            {
                for(int i = 1; i <= userInput; i++)
                {
                    sw.WriteLine(i + ", " + rand.Next(1, 10000));
                }
            }
        }
        #endregion

    }
}
