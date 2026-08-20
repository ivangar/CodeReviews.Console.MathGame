using MathGame.ivangar.Enums;

namespace MathGame.ivangar.Validators
{
    public static class MenuValidator
    {
        public static bool ValidateMainOptions(string? option)
        {
            if (string.IsNullOrEmpty(option) || string.IsNullOrWhiteSpace(option))
            {
                Menu.PrintMenu(true);
                return false;
            }

            var validOption = Enum.TryParse<MainMenuItems>(option.Trim(), ignoreCase: true, out _);

            if (!validOption)
            {
                Menu.PrintMenu(true);
            }

            return validOption;
        }
    }
}
