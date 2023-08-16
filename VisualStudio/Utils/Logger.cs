namespace FuelManager
{
    public class Logger
    {
        public static void Log(string message, params object[] parameters)              => Melon<FuelManager>.Logger.Msg($"{message}", parameters);
        public static void LogWarning(string message, params object[] parameters)       => Melon<FuelManager>.Logger.Warning($"{message}", parameters);
        public static void LogError(string message, params object[] parameters)         => Melon<FuelManager>.Logger.Error($"{message}", parameters);
        public static void LogSeperator(params object[] parameters)                     => Melon<FuelManager>.Logger.Msg("==============================================================================", parameters);
        public static void LogStarter()                                                 => Melon<FuelManager>.Logger.Msg($"Mod loaded with v{BuildInfo.Version}");
        public static void LogUpdate(string[] message)
        {
            LogSeperator();
            for (int i = 0; i < message.Length; i++)
            {
                Log(message[i]);
            }
            LogSeperator();
        }

        public static void LogWithLevel(LoggingLevel level, string message, params object[] parameters)
        {
            switch (level)
            {
                case LoggingLevel.None:
                    break;
                case LoggingLevel.Info:
                    break;
                case LoggingLevel.Debug:
                    break;
                case LoggingLevel.Warn:
                    break;
                case LoggingLevel.Error:
                    break;
                case LoggingLevel.Fatal:
                    break;
                case LoggingLevel.Trace:
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}