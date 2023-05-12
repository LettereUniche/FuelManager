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
        [Slider(1f, 60f)]
        public float refuelTime = 3f;

        [Section("Spawn Settings")]
        [Name("Pilgram / Very High Loot Custom")]
        [Description("Setting to zero disables them on this game mode.  Recommended is 40.")]
        [Slider(0f, 50f, 101)]
        public float pilgramSpawnExpectation = 40f;

        [Name("Voyager / High Loot Custom")]
        [Description("Setting to zero disables them on this game mode.  Recommended is 30.")]
        [Slider(0f, 50f, 101)]
        public float voyagerSpawnExpectation = 30f;

        [Name("Stalker / Medium Loot Custom")]
        [Description("Setting to zero disables them on this game mode.  Recommended is 15.")]
        [Slider(0f, 50f, 101)]
        public float stalkerSpawnExpectation = 15f;

        [Name("Interloper / Low Loot Custom")]
        [Description("Setting to zero disables them on this game mode.  Recommended is 5.")]
        [Slider(0f, 50f, 101)]
        public float interloperSpawnExpectation = 5f;

        [Name("Challenges")]
        [Description("Setting to zero disables them on this game mode.  Recommended is 40.")]
        [Slider(0f, 50f, 101)]
        public float challengeSpawnExpectation = 40f;

        protected override void OnConfirm()
        {
            base.OnConfirm();
            radialMenu!.SetValues(keyCode, enableRadial);
        }

        internal static void OnLoad()
        {
            _settings.AddToModSettings("Fuel Manager");
            radialMenu = new CustomRadialMenu(_settings.keyCode, CustomRadialMenuType.AllOfEach, new string[] { "GEAR_JerrycanRusty", "GEAR_LampFuel", "GEAR_LampFuelFull" }, _settings.enableRadial);
#if DEBUG
            FuelManager.Log("OnLoad");
#endif
        }
    }
}
