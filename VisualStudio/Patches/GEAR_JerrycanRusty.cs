namespace FuelManager
{
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Deserialize), new Type[] { typeof(GearItemSaveDataProxy) })]
    //[HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
    internal static class GearItem_Deserialize_JerrycanRusty
    {
        // "GEAR_JerrycanRusty"
        private static void Postfix(GearItem __instance)
        {
            if (__instance == null) return;

            if (__instance.DisplayName != null && __instance.DisplayName == "Jerry Can")
            {
                FuelItemAPI.AddRepair(__instance);
                FuelItemAPI.AddHarvest(__instance);
            }
        }
    }
}
