namespace FuelManager
{
    public class FuelItemAPI
    {

        /// <summary>
        /// This should only be used to add repair to already existing items. Otherwise use the Modcomponent function
        /// </summary>
        /// <param name="gi">The GearItem you want to add repair to</param>
        public static void AddRepair(GearItem gi)
        {
            gi.m_Repairable                          = ItemUtils.GetOrCreateComponent<Repairable>(gi.gameObject);

            gi.m_Repairable.m_ConditionIncrease      = 50f;
            gi.m_Repairable.m_RequiredGear           = new GearItem[] { ItemUtils.GetGearItemPrefab(FuelManager.SCRAP_METAL_NAME) };
            gi.m_Repairable.m_RequiredGearUnits      = new int[] { 1 };
            gi.m_Repairable.m_DurationMinutes        = 15;
            gi.m_Repairable.m_RepairAudio            = "Play_RepairingMetal";
            gi.m_Repairable.m_RepairToolChoices      = new ToolsItem[] { ItemUtils.GetToolItemPrefab("GEAR_HighQualityTools"), ItemUtils.GetToolItemPrefab("GEAR_SimpleTools") };
            gi.m_Repairable.m_RequiresToolToRepair   = true;
            gi.m_Repairable.m_NeverFail              = true;
        }

        /// <summary>
        /// This should only be used to add harvest to already existing items. Otherwise use the Modcomponent function
        /// </summary>
        /// <param name="gi">The GearItem you want to add harvest to</param>
        public static void AddHarvest(GearItem gi)
        {
            gi.m_Harvest = ItemUtils.GetOrCreateComponent<Harvest>(gi.gameObject);

            gi.m_Harvest.m_Audio            = "Play_HarvestingMetalSaw";
            gi.m_Harvest.m_DurationMinutes  = 15;
            gi.m_Harvest.m_YieldGear        = new GearItem[] { ItemUtils.GetGearItemPrefab(FuelManager.SCRAP_METAL_NAME) };
            gi.m_Harvest.m_YieldGearUnits   = new int[] { 2 };
            gi.m_Harvest.m_AppliedSkillType = SkillType.None;
            gi.m_Harvest.m_RequiredTools    = new ToolsItem[] { ItemUtils.GetToolItemPrefab("GEAR_Hacksaw") };
        }
    }
}
