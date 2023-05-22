namespace FuelManager
{
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
    internal static class GearItem_Awake_JerrycanRusty
    {
        private static void Postfix(GearItem __instance)
        {
            if (ItemUtils.NormalizeName(__instance.name) == "GEAR_JerrycanRusty")
            {
                __instance.m_Repairable = ItemUtils.GetOrCreateComponent<Repairable>(__instance.gameObject);

                __instance.m_Repairable.m_ConditionIncrease     = 50f;
                __instance.m_Repairable.m_RequiredGear          = new GearItem[] { ItemUtils.GetGearItemPrefab(FuelManager.SCRAP_METAL_NAME) };
                __instance.m_Repairable.m_RequiredGearUnits     = new int[] { 1 };
                __instance.m_Repairable.m_DurationMinutes       = 15;
                __instance.m_Repairable.m_RepairAudio           = "Play_RepairingMetal";
                __instance.m_Repairable.m_RepairToolChoices     = new ToolsItem[] { ItemUtils.GetToolItemPrefab("GEAR_HighQualityTools"), ItemUtils.GetToolItemPrefab("GEAR_SimpleTools") };
                __instance.m_Repairable.m_RequiresToolToRepair  = true;
                __instance.m_Repairable.m_NeverFail             = true;
            }
        }
    }
}
