namespace MathGame.ivangar.Validators
{
    public class GameValidator
    {
        public void ValidateAnswer(int result, int userAnswer, ref int score)
        {
            if (result == userAnswer)
            {
                Console.WriteLine("Correct Answer!");
                score++;
            }

            else
                Console.WriteLine($"Incorrect Answer! The correct answer is: {result}");
        }

        public bool ValidateContinueGame(char input)
        {
            while (input != 'y' && input != 'n')
            {
                input = Menu.ContinueGamePrompt(invalid: true);
            }

            return input == 'y';
        }
    }
}
