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
            if (gearItem.CurrentHP != gearItem.GetMaxHPFromRepair())
            {
                gearItem.CurrentHP = gearItem.GetMaxHPFromRepair();
            }
            //gearItem.CurrentHP = Mathf.Clamp(_Panel_Inventory_Examine.m_GearItem.CurrentHP, 0f, _Panel_Inventory_Examine.m_GearItem.GetMaxHPFromRepair());
            //_Panel_Inventory_Examine.m_GearItem.UpdateDamageShader();
        }
    }
}
