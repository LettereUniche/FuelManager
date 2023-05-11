namespace FuelManager
{
    using System;
    using System.Reflection;
    using Il2Cpp;
    using Il2CppTLD.Gear;
    using MelonLoader;
    using HarmonyLib;
    using ModSettings;
    using ModComponent;
    using UnityEngine;
    internal class Settings : JsonModSettings
    {
        internal static readonly Settings _settings = new();

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

        internal static void OnLoad()
        {
            _settings.AddToModSettings("Fuel Manager");
        }
    }
}
