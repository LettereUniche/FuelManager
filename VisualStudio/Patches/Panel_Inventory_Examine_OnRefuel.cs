namespace FuelManager.Patches
{
    using Il2Cpp;
    using HarmonyLib;
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.OnRefuel))]
    internal class Panel_Inventory_Examine_OnRefuel
    {
        private static bool Prefix(Panel_Inventory_Examine __instance)
        {
            //GearItem gearItem = __instance.m_GearItem;
            if (!FuelUtils.IsFuelItem(__instance.m_GearItem)) return true;

            if (ButtonUtils.IsSelected(__instance.m_Button_Unload))
            {
                FuelUtils.Drain(__instance.m_GearItem);
            }
            else
            {
                FuelUtils.Refuel(__instance.m_GearItem);
            }

            return false;
        }
    }
}
