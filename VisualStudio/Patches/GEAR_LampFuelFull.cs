namespace FuelManager
{
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
    internal static class GearItem_Deserialize_LampFuelFull
    {
        private static void Postfix(GearItem __instance)
        {
            if (ItemUtils.NormalizeName(__instance.name) == "gear_lampfuelfull")
            {
                FuelItemAPI.AddRepair(__instance);
                FuelItemAPI.AddHarvest(__instance);
            }
        }
    }
}
