using MathGame.ivangar.Enums;
using MathGame.ivangar.Helpers;

namespace MathGame.ivangar
{
    public class GameCenter
    {
        private readonly List<Game> _games = [];
        private bool _exitGame = false;

        public void Start()
        {
            string? menuOption = Menu.GameIntro();

            while (true)
            {
                while (!MenuValidator.ValidateMainOptions(menuOption))
                {
                    menuOption = Console.ReadLine();
                }

                Enum.TryParse<MainMenuItems>(menuOption!.Trim(), ignoreCase: true, out MainMenuItems option);

                switch (option)
                {
                    case MainMenuItems.Play:
                        PlayGame();
                        break;
                    case MainMenuItems.History:
                        MathGameHistory();
                        break;
                    case MainMenuItems.Exit:
                        _exitGame = true;
                        Exit();
                        break;
                }

                if (_exitGame)
                    break;

                Menu.PrintMenu();
                menuOption = Console.ReadLine();
            }
        }

        public void PlayGame()
        {
            var mathGame = new Game();
            mathGame.Play();
            _games.Add(mathGame);
        }

        public void MathGameHistory()
        {
            if (_games.Count == 0)
                Console.WriteLine("\nYou don't have any registered game played.\n");

            foreach (var (index, game) in _games.Select((g, i) => (i, g)))
            {
                Console.WriteLine($"\n\t\tGame #{index + 1}\n");
                game.PrintGameHistory();
                game.PrintScore();
            }
        }

        public void Exit()
        {
            if (_games.Count != 0)
            {
                Console.WriteLine("\nThank you for playing the Math Game. Here is your Math Game history\n\n");
                MathGameHistory();
            }

            else
                Console.WriteLine("\nThank you for playing the Math Game.");

            Console.WriteLine("\nHave a nice day!");
        }
    }
}
