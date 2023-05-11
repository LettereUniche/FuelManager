namespace FuelManager.Patches
{
    using Il2Cpp;
    using HarmonyLib;
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.OnUnload))]
    internal class Panel_Inventory_Examine_OnUnload
    {
        private static bool Prefix(Panel_Inventory_Examine __instance)
        {
            if (FuelUtils.IsFuelItem(__instance.m_GearItem.GetComponent<GearItem>()))
            {
                FuelUtils.Drain(__instance.m_GearItem.GetComponent<GearItem>());
                return false;
            }
            return true;
        }
    }
}
