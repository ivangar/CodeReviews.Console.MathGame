using MathGame.ivangar.Validators;

namespace MathGame.ivangar
{
    public class Game : IGame
    {
        private static readonly char[] _operators = ['+', '-', '*', '/'];

        private readonly int _maxNumberOfQuestions;

        // State fields
        private int _currentQuestionNumber;
        private int _score;
        private int _result;
        private int _op1, _op2;

        public int UserAnswer { get; set; }

        public IReadOnlyList<MathOperation> GameHistory => _gameHistory;

        private readonly List<MathOperation> _gameHistory = new();

        public Game()
        {
            _maxNumberOfQuestions = Random.Shared.Next(5, 11);
            _currentQuestionNumber = 1;
            _score = 0;
        }

        public void Play()
        {
            bool continueGame = true;
            char operation = Menu.StartGamePrompt(_maxNumberOfQuestions, _currentQuestionNumber);
            GameValidator validator = new GameValidator();

            while (continueGame)
            {
                while (!Array.Exists(_operators, o => o == operation))
                {
                    operation = Menu.PrintGameOptions(invalid: true);
                }

                PerformMathOperation(operation);
                validator.ValidateAnswer(_result, UserAnswer, ref _score);

                _currentQuestionNumber++;

                if (_currentQuestionNumber > _maxNumberOfQuestions)
                    break;

                continueGame = validator.ValidateContinueGame(Menu.ContinueGamePrompt());

                if (continueGame)
                    operation = Menu.PrintGameOptions(false, _currentQuestionNumber);
            }

            FinishGame();
        }

        public void PerformMathOperation(char operation)
        {
            Random random = new();

            _op1 = operation == '/' ? random.Next(1, 101) : random.Next(0, 101);
            _op2 = operation == '/' ? GetDivisor() : random.Next(0, 101);
            CalculateResult(operation);

            Console.Write($"\n\nWhat is the result of:\n{_op1} {operation} {_op2} = ");
            string? answer = Console.ReadLine();

            int parsedAnswer;
            while (!int.TryParse(answer, out parsedAnswer))
            {
                Console.WriteLine("Invalid input. Please enter a valid number: ");
                answer = Console.ReadLine();
            }

            UserAnswer = parsedAnswer;

            _gameHistory.Add(new MathOperation
            {
                OperandA = _op1,
                OperandB = _op2,
                Operation = operation,
                UserAnswer = UserAnswer,
                ScoreMark = UserAnswer == _result ? "Correct" : "Incorrect"
            }
            );
        }

        public void PrintGameHistory()
        {
            Console.WriteLine("\nGame History:\n");

            foreach (var (index, operation) in GameHistory.Select((o, i) => (i, o)))
            {
                Console.WriteLine($"{index + 1}. {operation}");
            }

            Console.WriteLine("\n\n");
        }

        public void PrintScore()
        {
            var numberOfQuestionsAnswered = _currentQuestionNumber - 1;
            decimal finalScore = Math.Round((decimal)_score * 100 / numberOfQuestionsAnswered, MidpointRounding.AwayFromZero);

            Console.WriteLine($"You answered correctly {_score}/{numberOfQuestionsAnswered} questions.");
            Console.WriteLine($"Your score is: {finalScore}%");
        }

        public void FinishGame()
        {
            Console.WriteLine("\n\nGame Over!\n");
            PrintGameHistory();
            PrintScore();
        }

        #region Private Methods
        private void CalculateResult(char operation)
        {
            _result = operation switch
            {
                '+' => _op1 + _op2,
                '-' => _op1 - _op2,
                '*' => _op1 * _op2,
                '/' => _op1 / _op2,
                _ => throw new InvalidOperationException("Invalid operation")
            };
        }

        /*Get a list of potential divisors (without remainders) and return randomly any divisor */
        private int GetDivisor()
        {
            if (IsPrimeNumber(_op1))
                return 1;

            var divisors = Enumerable
                .Range(1, _op1)
                .Where(x => _op1 % x == 0)
                .ToList();

            return divisors[Random.Shared.Next(0, divisors.Count)];
        }

        private static bool IsPrimeNumber(int number)
        {
            var primes = Enumerable.Range(2, 100)
                       .Where(n => !Enumerable.Range(2, (int)Math.Sqrt(n) - 1).Any(d => n % d == 0))
                       .ToList();

            return primes.Contains(number);
        }
        #endregion
    }
}
