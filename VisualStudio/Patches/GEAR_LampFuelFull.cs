namespace FuelManager
{
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
    internal static class GearItem_Awake_LampFuelFull
    {
        private static void Postfix(GearItem __instance)
        {
            if (ItemUtils.NormalizeName(__instance.name) == "GEAR_LampFuelFull")
            {
                __instance.m_Harvest = ItemUtils.GetOrCreateComponent<Harvest>(__instance.gameObject);

                __instance.m_Harvest.m_Audio = "Play_HarvestingMetalSaw";
                __instance.m_Harvest.m_DurationMinutes = 15;
                __instance.m_Harvest.m_YieldGear = new GearItem[] { ItemUtils.GetGearItemPrefab(FuelManager.SCRAP_METAL_NAME) };
                __instance.m_Harvest.m_YieldGearUnits = new int[] { 1 };
                __instance.m_Harvest.m_AppliedSkillType = SkillType.None;
                __instance.m_Harvest.m_RequiredTools = new ToolsItem[] { ItemUtils.GetToolItemPrefab("GEAR_Hacksaw") };
                __instance.m_Harvest.m_GunpowderYield = 0f;
            }
        }
    }
}
