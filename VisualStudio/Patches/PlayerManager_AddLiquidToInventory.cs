namespace FuelManager.Patches
{
    using System;
    using Il2Cpp;
    using HarmonyLib;
    [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.AddLiquidToInventory), new Type[] { typeof(float), typeof(GearLiquidTypeEnum) })]
    internal class PlayerManager_AddLiquidToInventory
    {
        private static void PostFix(PlayerManager __instance, float litersToAdd, GearLiquidTypeEnum liquidType, ref float __result)
        {
            if (liquidType == GearLiquidTypeEnum.Kerosene && __result != litersToAdd)
            {
                MessageUtils.SendLostMessageDelayed(litersToAdd - __result);

                // just pretend we added everything, so the original method will not generate new containers
                __result = litersToAdd;
            }
        }
    }
}
