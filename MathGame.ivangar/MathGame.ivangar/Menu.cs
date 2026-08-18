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

        public static void StartGamePrompt(int maxNumberOfQuestions)
        {
            Console.WriteLine($"\nThis game has {maxNumberOfQuestions} questions.");
        }

        /* TO DELETE */
        public static void PrintGameOptions(bool invalid = false, int currentQuestionNumber = -1)
        {
            if (invalid)
                Console.Write("\nInvalid operation selected! ");

            if (currentQuestionNumber > 0)
                Console.WriteLine($"\n\tQuestion #{currentQuestionNumber}\n");
        }
    }
}
