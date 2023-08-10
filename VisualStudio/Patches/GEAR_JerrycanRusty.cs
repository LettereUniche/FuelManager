namespace FuelManager
{
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Deserialize))]
    internal static class GearItem_Deserialize_JerrycanRusty
    {
        private static void Postfix(GearItem __instance)
        {
            if (ItemUtils.NormalizeName(__instance.name) == "GEAR_JerrycanRusty")
            {
                FuelItemAPI.AddRepair(__instance);
                FuelItemAPI.AddHarvest(__instance);
            }
        }
    }
}
