namespace MathGame.ivangar
{
    public class Game
    {
        private readonly char[] _operators = ['+', '-', '*', '/'];

        private int _maxNumberOfQuestions = Random.Shared.Next(5, 11);
        //private int _maxNumberOfQuestions = 4; //USE this to test, remove before PR

        private int _currentQuestionNumber = 1;

        private int _score = 0;

        private int _result;

        public int _userAnswer;

        private int _op1, _op2;

        public List<string> GameHistory = [];

        public void Start()
        {
            bool continueGame = true;
            Menu.StartGamePrompt(_maxNumberOfQuestions);
            var operation = GetNextOperation();

            while (continueGame)
            {
                switch (operation)
                {
                    case '+': Add(); break;
                    case '-': Subtract(); break;
                    case '*': Multiply(); break;
                    case '/': Divide(); break;
                }

                var validAnswer = ValidateAnswer();
                UpdateGameHistory(operation, validAnswer);
                _currentQuestionNumber++;

                if (_currentQuestionNumber > _maxNumberOfQuestions)
                    break;

                Console.WriteLine("\nWould you like to continue the game? (yes/no)");
                string? continueInput = Console.ReadLine();

                continueGame = ValidateContinueGame(continueInput);

                if (continueGame)
                    operation = GetNextOperation();
            }

            Console.WriteLine("\nGame Over!\n");
            PrintScore();
            PrintGameHistory();
        }

        public void Add()
        {
            _op1 = Random.Shared.Next(0, 101);
            _op2 = Random.Shared.Next(0, 101);
            _result = _op1 + _op2;

            Console.Write($"\n\nWhat is the result of:\n{_op1} + {_op2} = ");
            string? answer = Console.ReadLine();

            while (!int.TryParse(answer, out _userAnswer))
            {
                Console.WriteLine("Invalid input. Please enter a valid number: ");
                answer = Console.ReadLine();
            }
        }

        public void Subtract()
        {
            _op1 = Random.Shared.Next(0, 101);
            _op2 = Random.Shared.Next(0, 101);
            _result = _op1 - _op2;

            Console.Write($"\n\nWhat is the result of:\n{_op1} - {_op2} = ");
            string? answer = Console.ReadLine();

            while (!int.TryParse(answer, out _userAnswer))
            {
                Console.WriteLine("Invalid input. Please enter a valid number: ");
                answer = Console.ReadLine();
            }
        }

        public void Multiply()
        {
            _op1 = Random.Shared.Next(0, 101);
            _op2 = Random.Shared.Next(0, 101);
            _result = _op1 * _op2;

            Console.Write($"\n\nWhat is the result of:\n{_op1} * {_op2} = ");
            string? answer = Console.ReadLine();

            while (!int.TryParse(answer, out _userAnswer))
            {
                Console.WriteLine("Invalid input. Please enter a valid number: ");
                answer = Console.ReadLine();
            }
        }

        public void Divide()
        {
            _op1 = Random.Shared.Next(1, 101);
            _op2 = GetDivisor();
            _result = _op1 / _op2;

            Console.Write($"\n\nWhat is the result of:\n{_op1} / {_op2} = ");
            string? answer = Console.ReadLine();

            while (!int.TryParse(answer, out _userAnswer))
            {
                Console.WriteLine("Invalid input. Please enter a valid number: ");
                answer = Console.ReadLine();
            }
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
            decimal finalScore = Math.Round((decimal)_score * 100 / _maxNumberOfQuestions, MidpointRounding.AwayFromZero);
            Console.WriteLine($"Your score is: {finalScore}%");
        }

        private bool ValidateAnswer()
        {
            if (_result == _userAnswer)
            {
                Console.WriteLine("Correct Answer!");
                _score++;
                return true;
            }

            else
                Console.WriteLine($"Incorrect Answer! The correct answer is: {_result}");

            return false;
        }

        private void UpdateGameHistory(char operation, bool validAnswer)
        {
            var scoreMark = validAnswer ? "Correct" : "Incorrect";
            var operationLog = $"{_op1} {operation} {_op2} = {_userAnswer,-30} {scoreMark,-20}";
            GameHistory.Add(operationLog);
        }

        private bool ValidateContinueGame(string? input)
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

        private char GetNextOperation()
        {
            Console.WriteLine($"\n\n\tQuestion #{_currentQuestionNumber}\n");
            return _operators[Random.Shared.Next(0, _operators.Length)];
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

        private bool IsPrimeNumber(int number)
        {
            var primes = Enumerable.Range(2, 100)
                       .Where(n => !Enumerable.Range(2, (int)Math.Sqrt(n) - 1).Any(d => n % d == 0))
                       .ToList();

            return primes.Contains(number);
        }

    }
}
