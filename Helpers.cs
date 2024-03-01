using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

internal class Helpers
{
    public static string? Ask(string question)
    {
        Console.WriteLine(question);
        return Console.ReadLine();
    }

    public static string AskNotEmpty(string question)
    {
        string? retVal = null;
        do
        {
            retVal = Ask(question);
        }
        while (retVal == null || retVal == "");

        return retVal;
    }
    internal static int AskForInt(string question)
    {
        bool isInt = false;
        int result = 0;
        while (!isInt)
        {
            string? userInput = Ask(question);
            isInt = int.TryParse(userInput, out result);

            if (isInt)
            {
                Debug.WriteLine("Parsen gelukt!");
            }
            else
            {
                Debug.WriteLine("Parsen niet gelukt!");
            }
        }

        return result;
    }

    internal static decimal AskForDecimal(string question)
    {
        bool isDecimal = false;
        decimal result = 0;
        while (!isDecimal)
        {
            string? userInput = Ask(question);
            isDecimal = decimal.TryParse(userInput, out result);

            if (isDecimal)
            {
                Debug.WriteLine("Parsen gelukt!");
            }
            else
            {
                Debug.WriteLine("Parsen niet gelukt!");
            }
        }

        return result;
    }

    internal static void Wait()
    {
        Console.WriteLine("Press <ENTER> to continue...");
        Console.ReadLine();
    }

    internal static string Choose(string question, string[] options)
    {
        int choice = 0;
        bool isCorrectChoice = false;

        while (!isCorrectChoice)
        {
            // Show all the options with an index in front of it
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            // Ask the question
            choice  = AskForInt(question);

            // Check if the answer is in the range of the options
            if (choice >= 1 && choice  <= options.Length)
            {
                isCorrectChoice = true;
            }
            else
            {
                Console.WriteLine("Please choose one of the options");
            }
        }

        // return the chosen option
        return options[choice - 1];
    }

    public static string AskPassword(string question)
    {
        string retVal = "";

        while (retVal == "")
        {
            Console.WriteLine(question);

            // Get the key from the user, until the user presses enter
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // If the key is not enter, add the key to the return value
                if (key.Key != ConsoleKey.Enter)
                {
                    retVal += key.KeyChar;
                    Console.Write("*");
                }
            }
            while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
        }

        return retVal;
    }

    public static string HashPassword(string password)

    {

        using (var sha256 = System.Security.Cryptography.SHA256.Create())

        {

            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

        }

    }
}

