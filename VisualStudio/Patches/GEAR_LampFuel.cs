
namespace FuelManager
{
    //[HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Deserialize), new Type[] { typeof(GearItemSaveDataProxy) })]
    internal static class GearItem_Deserialize_LampFuel
    {
        // "GEAR_LampFuel"
        private static void Postfix(GearItem __instance)
        {
            if (__instance == null) return;

            if (__instance.DisplayName != null && __instance.DisplayName == "Lantern Fuel")
            {
                FuelItemAPI.AddRepair(__instance);
                FuelItemAPI.AddHarvest(__instance);
            }
        }
    }
}
