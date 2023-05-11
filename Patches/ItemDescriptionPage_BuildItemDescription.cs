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
    [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.BuildItemDescription), new Type[] { typeof(GearItem) } )]
    internal class ItemDescriptionPage_BuildItemDescription
    {
        private static void PostFix(GearItem gi)
        {
            if (FuelUtils.IsFuelContainer(gi))
            {
                ItemUtils.SetConditionToMax(gi);
            }
        }
    }
}
