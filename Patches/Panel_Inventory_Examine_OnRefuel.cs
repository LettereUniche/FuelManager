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
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.OnRefuel))]
    internal class Panel_Inventory_Examine_OnRefuel
    {
        private static bool Prefix(Panel_Inventory_Examine __instance)
        {
            if (!FuelUtils.IsFuelItem(__instance.m_GearItem)) return true;

            if (ButtonUtils.IsSelected(__instance.m_Button_Unload))
            {
                FuelUtils.Drain(__instance.m_GearItem);
            }
            else
            {
                FuelUtils.Refuel(__instance.m_GearItem);
            }

            return false;
        }
    }
}
