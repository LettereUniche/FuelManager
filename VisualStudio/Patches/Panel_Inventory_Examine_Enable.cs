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
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.Enable), new Type[] { typeof(bool), typeof(ComingFromScreenCategory) })]
    internal class Panel_Inventory_Examine_Enable
    {
        private static void Prefix(Panel_Inventory_Examine __instance, bool enable)
        {
            if (!enable || __instance == null) return;

            if (FuelUtils.IsFuelItem(__instance.m_GearItem))
            {
                // repurpose the left "Unload" button to "Drain"
                ButtonUtils.SetButtonLocalizationKey(__instance.m_Button_Unload, "GAMEPLAY_BFM_Drain");
                ButtonUtils.SetButtonSprite(__instance.m_Button_Unload, "ico_lightSource_lantern");
                // rename the bottom right "Unload" button to "Drain"
                ButtonUtils.SetUnloadButtonLabel(__instance, "GAMEPLAY_BFM_Drain");

                Transform lanternTexture = __instance.m_RefuelPanel.transform.Find("FuelDisplay/Lantern_Texture");
                ButtonUtils.SetTexture(lanternTexture, Utils.GetInventoryIconTexture(__instance.m_GearItem));
            }
            else
            {
                ButtonUtils.SetButtonLocalizationKey(__instance.m_Button_Unload, "GAMEPLAY_Unload");
                ButtonUtils.SetButtonSprite(__instance.m_Button_Unload, "ico_ammo_rifle");
                ButtonUtils.SetUnloadButtonLabel(__instance, "GAMEPLAY_Unload");
            }
        }
    }
}
