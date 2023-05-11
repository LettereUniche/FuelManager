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
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.RefreshMainWindow))]
    internal class Panel_Inventory_Examine_RefreshMainWindow
    {
        private static void Postfix(Panel_Inventory_Examine __instance)
        {
            if (__instance == null || __instance.m_GearItem == null || !FuelUtils.IsFuelItem(__instance.m_GearItem)) return;

            Vector3 position = ButtonUtils.GetBottomPosition(
                __instance.m_Button_Harvest,
                __instance.m_Button_Refuel,
                __instance.m_Button_Repair);
            position.y += __instance.m_ButtonSpacing;
            __instance.m_Button_Unload.transform.localPosition = position;

            __instance.m_Button_Unload.gameObject.SetActive(true);

            float litersToDrain = FuelUtils.GetLitersToDrain(__instance.m_GearItem);
            __instance.m_Button_Unload.GetComponent<Panel_Inventory_Examine_MenuItem>().SetDisabled(litersToDrain < FuelUtils.MIN_LITERS);
            //if (litersToDrain < FuelUtils.MIN_LITERS) __instance.m_Button_Unload.GetComponent<Panel_Inventory_Examine_MenuItem>().SetDisabled(true);
            //if (litersToDrain < FuelUtils.MIN_LITERS) __instance.m_Button_Unload.gameObject.SetActive(false);
        }
    }
}
