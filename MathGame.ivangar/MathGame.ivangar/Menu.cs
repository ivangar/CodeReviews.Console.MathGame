using MathGame.ivangar.Enums;

namespace MathGame.ivangar
{
    public static class Menu
    {
        public static string? GameIntro()
        {
            Console.WriteLine("\nWelcome to the Math Game!");
            Console.WriteLine("We are going to test your math skills!");
            PrintMenu();
            return Console.ReadLine();
        }

        public static void PrintMenu(bool invalid = false)
        {
            if (invalid)
                Console.WriteLine("\nInvalid input.");

            Console.WriteLine("\nPlease choose any of the following options (you have to type the word, i.e. 'play'):\n");
            PrintMenuOptions();
        }

        public static void PrintMenuOptions()
        {
            foreach (MainMenuItems option in Enum.GetValues(typeof(MainMenuItems)))
                Console.WriteLine($"\t{(int)option}. {option}");

            Console.WriteLine("\n");
        }

        public static char StartGamePrompt(int maxNumberOfQuestions, int currentQuestionNumber)
        {
            Console.WriteLine($"\nThis game has {maxNumberOfQuestions} questions.");
            return PrintGameOptions(invalid: false, currentQuestionNumber);
        }

        public static char PrintGameOptions(bool invalid = false, int currentQuestionNumber = -1)
        {
            if (invalid)
                Console.Write("\nInvalid operation selected! ");

            if (currentQuestionNumber > 0)
                Console.WriteLine($"\n\n\tQuestion #{currentQuestionNumber}\n");

            Console.Write("Choose an operation and type any of the following options: +, -, *, / \n");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            return keyInfo.KeyChar;
        }

        public static char ContinueGamePrompt(bool invalid = false)
        {
            if (invalid)
                Console.Write("\nInvalid input. Please enter 'y' or 'n': ");

            else
                Console.Write("\nWould you like to continue the game? (y/n): ");

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            char continueInput = char.ToLower(keyInfo.KeyChar);
            return continueInput;
        }
    }
}
