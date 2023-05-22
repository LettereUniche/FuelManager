namespace FuelManager
{
    using ModSettings;
    using RadialMenuUtilities;
    using UnityEngine;

    internal class Settings : JsonModSettings
    {
        internal static readonly Settings _settings = new();
        internal static CustomRadialMenu? radialMenu;

        [Section("Gameplay Settings")]
        [Name("Use Radial Menu")]
        [Description("Enables a new radial menu for you to easily access your fuel containers.")]
        public bool enableRadial = false;

        [Name("Key for Radial Menu")]
        [Description("The key you press to show the new menu.")]
        public KeyCode keyCode = KeyCode.G;

        [Section("Main")]
        [Name("Refuel Time")]
        [Description("How much time it takes to refuel/ drain, in seconds. Default: 3")]
        [Slider(1f, 60f, 240)]
        public float refuelTime = 3f;

        [Section("Spawn Settings")]
        [Name("Pilgram / Very High Loot Custom")]
        [Description("Setting to zero disables them on this game mode")]
        [Slider(0f, 100f, 101)]
        public float pilgramSpawnExpectation = 70f;

        [Name("Voyager / High Loot Custom")]
        [Description("Setting to zero disables them on this game mode")]
        [Slider(0f, 100f, 101)]
        public float voyagerSpawnExpectation = 40f;

        [Name("Stalker / Medium Loot Custom")]
        [Description("Setting to zero disables them on this game mode")]
        [Slider(0f, 100f, 101)]
        public float stalkerSpawnExpectation = 20f;

        [Name("Interloper / Low Loot Custom")]
        [Description("Setting to zero disables them on this game mode")]
        [Slider(0f, 100f, 101)]
        public float interloperSpawnExpectation = 8f;

        [Name("Challenges")]
        [Description("Setting to zero disables them on this game mode")]
        [Slider(0f, 100f, 101)]
        public float challengeSpawnExpectation = 50f;

        protected override void OnConfirm()
        {
            base.OnConfirm();
            radialMenu!.SetValues(keyCode, enableRadial);
        }

        internal static void OnLoad()
        {
            _settings.AddToModSettings("Fuel Manager");
            radialMenu = new CustomRadialMenu(_settings.keyCode, CustomRadialMenuType.AllOfEach, new string[] { "GEAR_GasCan", "GEAR_GasCanFull", "GEAR_JerrycanRusty", "GEAR_LampFuel", "GEAR_LampFuelFull" }, _settings.enableRadial);
#if DEBUG
            FuelManager.Log("OnLoad");
#endif
        }
    }
}
