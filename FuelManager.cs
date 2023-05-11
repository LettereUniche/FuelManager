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
    internal class FuelManager : MelonMod
    {
        public override void OnInitializeMelon()
        {
            Settings.OnLoad();
            Spawns.AddToModComponent();
        }
    }
}