namespace FuelManager
{
    internal class FuelManager : MelonMod
    {
        internal static string SCRAP_METAL_NAME     = "GEAR_ScrapMetal";
        internal static string SIMPLE_TOOLS_NAME    = "GEAR_HighQualityTools";
        internal static string QUALITY_TOOLS_NAME   = "GEAR_SimpleTools";
        internal static string[] FuelContainerWhiteList = { "GEAR_GasCan", "GEAR_GasCanFull", "GEAR_JerrycanRusty", "GEAR_LampFuel", "GEAR_LampFuelFull" };

        public override void OnInitializeMelon()
        {
            Settings.OnLoad();
            Spawns.AddToModComponent();
#if DEBUG
            Log($"Version: {BuildInfo.Version} Loaded!");
#endif
        }

        internal static void Log(string message, params object[] parameters)
        {
            MelonLogger.Msg($"{message}", parameters);
        }
        internal static void LogWarning(string message, params object[] parameters)
        {
            MelonLogger.Warning($"{message}", parameters);
        }
        internal static void LogError(string message, params object[] parameters)
        {
            MelonLogger.Error($"{message}", parameters);
        }
    }
}