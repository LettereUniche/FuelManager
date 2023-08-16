namespace FuelManager
{
    //[HarmonyPatch(typeof(GearItem), nameof(GearItem.Deserialize), new Type[] { typeof(GearItemSaveDataProxy) })]
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
    internal static class GearItem_Deserialize_JerrycanRusty
    {
        private static void Postfix(GearItem __instance)
        {
            if (ItemUtils.NormalizeName(__instance.name) == "gear_jerrycanrusty")
            {
                FuelItemAPI.AddRepair(__instance);
                FuelItemAPI.AddHarvest(__instance);
            }
        }
    }
}
