namespace FuelManager
{
    internal class FuelManager : MelonMod
    {
        public override void OnInitializeMelon()
        {
            Settings.OnLoad();
            Spawns.AddToModComponent();
#if DEBUG
            Log($"Version: {Assembly.GetExecutingAssembly().GetName().Version} Loaded!");
#endif
        }

        internal static void Log(string message, params object[] parameters)
        {
            MelonLogger.Msg($"[FuelManager] {message}", parameters);
        }
        internal static void LogWarning(string message, params object[] parameters)
        {
            MelonLogger.Warning($"[FuelManager] {message}", parameters);
        }
        internal static void LogError(string message, params object[] parameters)
        {
            MelonLogger.Error($"[FuelManager] {message}", parameters);
        }
    }
}