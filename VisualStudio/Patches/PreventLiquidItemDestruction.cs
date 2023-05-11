namespace FuelManager.Patches
{
    using System;
    using Il2Cpp;
    using HarmonyLib;
    using UnityEngine;
    internal static class PreventLiquidItemDestruction
    {
        internal static int deductLiquidFromInventoryCallDepth = 0;

        [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.DeductLiquidFromInventory))]
        internal static class PlayerManager_DeductLiquidFromInventory
        {
            private static void Prefix()
            {
                deductLiquidFromInventoryCallDepth++;
            }
            private static void Postfix()
            {
                deductLiquidFromInventoryCallDepth--;
            }
        }

        [HarmonyPatch(typeof(Inventory), nameof(Inventory.DestroyGear), new Type[] { typeof(GameObject) })]
        internal static class Inventory_DestroyGear
        {
            private static bool Prefix(GameObject go)
            {
                if (deductLiquidFromInventoryCallDepth > 0)
                {
                    LiquidItem liquidItem = go.GetComponent<LiquidItem>();

                    if (liquidItem != null && liquidItem.m_LiquidType == GearLiquidTypeEnum.Kerosene)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
