namespace FuelManager.Patches
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
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.OnUnload))]
    internal class Panel_Inventory_Examine_OnUnload
    {
        private static bool Prefix(Panel_Inventory_Examine __instance)
        {
            if (FuelUtils.IsFuelItem(__instance.m_GearItem))
            {
                FuelUtils.Drain(__instance.m_GearItem);
                return false;
            }
            return true;
        }
    }
}
