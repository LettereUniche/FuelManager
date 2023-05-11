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
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.UpdateButtonLegend))]
    internal class Panel_Inventory_Examine_UpdateButtonLegend
    {
        private static void Postfix(Panel_Inventory_Examine __instance)
        {
            if (FuelUtils.IsFuelItem(__instance.m_GearItem) && ButtonUtils.IsSelected(__instance.m_Button_Unload))
            {
                __instance.m_ButtonLegendContainer.UpdateButton("Continue", "GAMEPLAY_BFM_Drain", true, 1, true);
            }
        }
    }
}
