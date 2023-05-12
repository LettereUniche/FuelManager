namespace FuelManager.Utils
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
    internal class Utilities
    {
        internal static GearItemData _GearItemData = new();
        internal static Panel_Inventory_Examine _Panel_Inventory_Examine = new();
        internal static void SetConditionToMax(GearItem gearItem)
        {
            if (gearItem.CurrentHP < gearItem.GetMaxHPFromRepair())
            {
                gearItem.SetHaltDecay(true);
                float num = gearItem.GetMaxHPFromRepair();

                gearItem.CurrentHP += num;
                gearItem.CurrentHP = Mathf.Clamp(gearItem.CurrentHP, 0f, num);

                gearItem.UpdateDamageShader();

                gearItem.SetHaltDecay(false);

                _Panel_Inventory_Examine.RefreshButton();
            }
        }
    }
}
