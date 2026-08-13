namespace MathGame.ivangar
{
    public class Game
    {
        private readonly char[] _operators = ['+', '-', '*', '/'];

        //private int _maxNumberOfQuestions = Random.Shared.Next(5, 11);
        private int _maxNumberOfQuestions = 2;

        private int _currentQuestionNumber = 1;

        private int _score = 0;

        public List<string> GameHistory = [];

        public void Start()
        {
            bool continueGame = true;
            char operation = Menu.StartGamePrompt(_maxNumberOfQuestions, _currentQuestionNumber);

            while (continueGame)
            {
                while (!Array.Exists(_operators, o => o == operation))
                {
                    operation = Menu.PrintGameOptions(true);
                }

                var (result, answer) = operation switch
                {
                    '+' => Add(),
                    '-' => Subtract(),
                    _ => throw new InvalidOperationException("Invalid operation selected.")
                };

                ValidateAnswer(result, answer);
                _currentQuestionNumber++;

                if (_currentQuestionNumber > _maxNumberOfQuestions)
                    break;

                Console.WriteLine("\nWould you like to continue the game? (yes/no)");
                string? continueInput = Console.ReadLine();

                continueGame = ValidateContinueGame(continueInput);

                if (continueGame)
                    operation = Menu.PrintGameOptions(false, _currentQuestionNumber);
            }

            Console.WriteLine("\nGame Over!\n");
            PrintScore();
            PrintGameHistory();
        }

        public (int result, int answer) Add()
        {
            var a = Random.Shared.Next(0, 101);
            var b = Random.Shared.Next(0, 101);
            var result = a + b;

            Console.Write($"\n\nWhat is the result of:\n{a} + {b} = ");
            string? answer = Console.ReadLine();
            int parsedAnswer;

            while (!int.TryParse(answer, out parsedAnswer))
            {
                Console.WriteLine("Invalid input. Please enter a valid number: ");
                answer = Console.ReadLine();
            }

            GameHistory.Add($"{a} + {b} = {parsedAnswer}");

            return (result, parsedAnswer);
        }

        public (int result, int answer) Subtract()
        {
            var a = Random.Shared.Next(0, 101);
            var b = Random.Shared.Next(0, 101);
            var result = a - b;

            Console.Write($"\n\nWhat is the result of:\n{a} - {b} = ");
            string? answer = Console.ReadLine();
            int parsedAnswer;

            while (!int.TryParse(answer, out parsedAnswer))
            {
                Console.WriteLine("Invalid input. Please enter a valid number: ");
                answer = Console.ReadLine();
            }

            GameHistory.Add($"{a} - {b} = {parsedAnswer}");

            return (result, parsedAnswer);
        }

        public void ValidateAnswer(int result, int answer)
        {
            if (result == answer)
            {
                Console.WriteLine("Correct Answer!");
                _score++;
            }
            else
            {
                Console.WriteLine($"Incorrect Answer! The correct answer is: {result}");
            }
        }

        public void PrintGameHistory()
        {
            Console.WriteLine("\nGame History:\n");
            foreach (var (index, operation) in GameHistory.Select((o, i) => (i, o)))
            {
                Console.WriteLine($"{index + 1}. {operation}");
            }
        }

        public bool ValidateContinueGame(string? input)
        {
            while (string.IsNullOrWhiteSpace(input) ||
                (!string.Equals(input.Trim(), "yes", StringComparison.OrdinalIgnoreCase) &&
                 !string.Equals(input.Trim(), "no", StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Invalid input. Please enter 'yes' or 'no': ");
                input = Console.ReadLine();
            }

            return string.Equals(input.Trim(), "yes", StringComparison.OrdinalIgnoreCase);
        }

        public void PrintScore()
        {
            decimal finalScore = Math.Round((decimal)_score * 100 / _maxNumberOfQuestions, MidpointRounding.AwayFromZero);
            Console.WriteLine($"Your score is: {finalScore}%");
        }
    }
}
