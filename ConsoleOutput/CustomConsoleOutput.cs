
namespace DITest.Output
{
    /// <summary>
    /// Utility class for managing console colors and text formatting.
    /// </summary>
    public static class CustomConsole
    {
        public enum ConsoleTypeEnum
        {
            Debug,
            Busy,
            Released,
            Exit,
        }

        private static Dictionary<ConsoleTypeEnum, string> colorMap = new Dictionary<ConsoleTypeEnum, string>
        {
            { ConsoleTypeEnum.Debug, "\u001b[33m[DEBUG]\u001b[0m" },
            { ConsoleTypeEnum.Busy, "\u001b[31m[Busy]\u001b[0m" },
            { ConsoleTypeEnum.Released, "\u001b[32m[Released]\u001b[0m" },
            { ConsoleTypeEnum.Exit, "\u001b[34m[Exit]\u001b[0m" },
        };

        private static string GetFormattedString(ConsoleTypeEnum colorEnum)
        {
            if (colorMap.ContainsKey(colorEnum))
                return colorMap[colorEnum];
            else
                return "";
        }

        public static string LogToCustomConsole(int counterValue, ConsoleTypeEnum consoleType, int threadId = 0)
        {
            string formattedString = "";

            switch (consoleType)
            {
                case ConsoleTypeEnum.Debug:
                    formattedString = $"{counterValue}> {GetFormattedString(ConsoleTypeEnum.Debug)} Поток {threadId} создан.";
                    break;

                case ConsoleTypeEnum.Busy:
                    formattedString = $"{counterValue}> {GetFormattedString(ConsoleTypeEnum.Busy)} Поток {threadId} получил доступ к ресурсу и начал работу.";
                    break;

                case ConsoleTypeEnum.Released:
                    formattedString = $"{counterValue}> {GetFormattedString(ConsoleTypeEnum.Released)} Поток {threadId} освободил ресурс.";
                    break;

                case ConsoleTypeEnum.Exit:
                    formattedString = $"{counterValue}> {GetFormattedString(ConsoleTypeEnum.Exit)} Поток {threadId} Работа завершена.";
                    break;

                default:
                    break;
            }

            Console.WriteLine(formattedString);
            return formattedString;
        }
    }
}