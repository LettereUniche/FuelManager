namespace FuelManager
{
    internal class FuelManager : MelonMod
    {
        public static class BuildInfo
        {
            #region Mandatory
            /// <summary>The machine readable name of the mod (no special characters or spaces)</summary>
            public const string Name            = "FuelManager";
            /// <summary>Who made the mod</summary>
            public const string Author          = "The Illusion";
            /// <summary>Current version (Using Major.Minor.Build) </summary>
            public const string Version         = "1.0.3";
            #endregion

            #region Optional
            /// <summary>What the mod does</summary>
            public const string Description     = "Allows draining laterns and moving fuel between containers";
            /// <summary>Company that made it</summary>
            public const string Company         = null;
            /// <summary>A valid download link</summary>
            public const string DownloadLink    = "https://github.com/Arkhorse/FuelManager/releases/latest";
            /// <summary>Copyright info</summary>
            public const string Copyright       = "2023";
            /// <summary>Trademark info</summary>
            public const string Trademark       = null;
            /// <summary>Product name (should always be the same as the name)</summary>
            public const string Product         = "FuelManager";
            /// <summary>Culture info</summary>
            public const string Culture         = null;
            #endregion
        }
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