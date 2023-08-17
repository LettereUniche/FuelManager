namespace FuelManager
{
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Deserialize), new Type[] { typeof(GearItemSaveDataProxy) })]
    internal static class GearItem_Deserialize_LampFuelFull
    {
        // "GEAR_LampFuelFull"
        private static void Postfix(GearItem __instance)
        {
            if (__instance == null) return;

            if (__instance.name!= null && (__instance.name == "GEAR_LampFuelFull" || __instance.name == "GEAR_LampFuelFull(Clone)") )
            {
                FuelItemAPI.AddRepair(__instance);
                FuelItemAPI.AddHarvest(__instance);
            }
        }
    }
}
