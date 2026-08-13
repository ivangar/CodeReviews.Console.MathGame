using MathGame.ivangar.Enums;

namespace MathGame.ivangar
{
    public static class Menu
    {
        public static string? Intro()
        {
            Console.WriteLine("Welcome to the Math Game!");
            Console.WriteLine("We are going to test your math skills!");
            PrintMenu();
            return Console.ReadLine();
        }

        public static void PrintMenu(bool invalid = false)
        {
            if (invalid)
                Console.WriteLine("Invalid input.");

            Console.WriteLine("Please choose any of the following options (you have to type the word, i.e. 'play'):\n");
            PrintMenuOptions();
        }

        public static void PrintMenuOptions()
        {
            foreach (MainMenuItems option in Enum.GetValues(typeof(MainMenuItems)))
                Console.WriteLine($"\t{(int)option}. {option}");
        }

        public static char StartGamePrompt(int maxNumberOfQuestions, int currentQuestionNumber)
        {
            Console.WriteLine($"\nThis game has {maxNumberOfQuestions} questions.\n\n\tQuestion #{currentQuestionNumber}\n");
            return PrintGameOptions();
        }

        public static char PrintGameOptions(bool invalid = false, int currentQuestionNumber = -1)
        {
            if (invalid)
                Console.Write("\nInvalid operation selected! ");

            if (currentQuestionNumber > 0)
                Console.WriteLine($"\n\tQuestion #{currentQuestionNumber}\n");

            Console.Write("Choose an operation and type any of the following options: +, -, *, / ");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            return keyInfo.KeyChar;
        }
    }
}
