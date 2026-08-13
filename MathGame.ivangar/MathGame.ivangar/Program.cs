using MathGame.ivangar;
using MathGame.ivangar.Enums;
using MathGame.ivangar.Helpers;

List<Game> games = [];
bool exitGame = false;
string? menuOption = Menu.Intro();

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
            var game = new Game();
            game.Start();
            break;
        case MainMenuItems.History:
            Console.WriteLine("PRINT HISTORY");
            break;
        case MainMenuItems.Exit:
            exitGame = true;
            break;
    }

    if (exitGame)
    {
        Console.WriteLine("Thank you for playing the Math Game. Have a nice day!");
        break;
    }

    Menu.PrintMenu();
    menuOption = Console.ReadLine();
}