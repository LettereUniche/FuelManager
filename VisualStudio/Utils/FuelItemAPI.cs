namespace FuelManager
{
    public class FuelItemAPI
    {
        /// <summary>
        /// This should only be used to add repair to already existing items. Otherwise use the Modcomponent function
        /// </summary>
        /// <param name="gi">The GearItem you want to add repair to</param>
        public static void AddRepair(GearItem gi, bool NeverFail = false, float ConditionIncrease = 50f)
        {
            if (gi == null) return;
            Repairable repairable = ItemUtils.GetOrCreateComponent<Repairable>(gi.gameObject);

            repairable.m_RepairAudio = "Play_RepairingMetal";
            repairable.m_DurationMinutes = 15;
            repairable.m_ConditionIncrease = ConditionIncrease;

            repairable.m_RequiredGear = ItemUtils.GetItems<GearItem>(Constants.REPAIR_HARVEST_GEAR);
            repairable.m_RequiredGearUnits = new int[] { 1 };

            repairable.m_RepairToolChoices = ItemUtils.GetItems<ToolsItem>(Constants.REPAIR_TOOLS);
            repairable.m_RequiresToolToRepair = true;
            repairable.m_NeverFail = NeverFail;
        }

        /// <summary>
        /// This should only be used to add harvest to already existing items. Otherwise use the Modcomponent function
        /// </summary>
        /// <param name="gi">The GearItem you want to add harvest to</param>
        public static void AddHarvest(GearItem gi)
        {
            if (gi == null) return;

            Harvest harvest = ItemUtils.GetOrCreateComponent<Harvest>(gi.gameObject);

            harvest.m_Audio = "Play_HarvestingMetalSaw";
            harvest.m_DurationMinutes = 15;

            harvest.m_YieldGear = ItemUtils.GetItems<GearItem>(Constants.REPAIR_HARVEST_GEAR);
            harvest.m_YieldGearUnits = new int[] { 2 };

            harvest.m_AppliedSkillType = SkillType.None;
            harvest.m_RequiredTools = ItemUtils.GetItems<ToolsItem>(Constants.HARVEST_TOOLS);
            harvest.m_GunpowderYield = 0f;
        }
        /// <summary>
        /// Adds the FuelSourceItem component to the given GearItem
        /// </summary>
        /// <param name="gi">GearItem to alter</param>
        /// <param name="burnHours">m_BurnDurationHours</param>
        /// <param name="fireAge">m_FireAgeMinutesBeforeAdding</param>
        /// <param name="fireStartDuration">m_FireStartDurationModifier</param>
        /// <param name="fireStartSkill">m_FireStartSkillModifier</param>
        /// <param name="heatIncrease">m_HeatIncrease</param>
        /// <param name="heatInner">m_HeatInnerRadius</param>
        /// <param name="heatOuter">m_HeatOuterRadius</param>
        /// <param name="isBurnt">m_IsBurntInFireTracked</param>
        /// <param name="isTinder">m_IsTinder</param>
        /// <param name="isWet">m_IsWet</param>
        public static void AddFuelSource(GearItem gi,
                                         float burnHours,
                                         float fireAge,
                                         float fireStartDuration,
                                         float fireStartSkill,
                                         float heatIncrease,
                                         float heatInner,
                                         float heatOuter,
                                         bool isBurnt,
                                         bool isTinder,
                                         bool isWet)
        {
            FuelSourceItem fuelSourceItem = ItemUtils.GetOrCreateComponent<FuelSourceItem>(gi.gameObject);

            fuelSourceItem.m_BurnDurationHours = burnHours;

            fuelSourceItem.m_FireAgeMinutesBeforeAdding = fireAge;
            fuelSourceItem.m_FireStartDurationModifier = fireStartDuration;
            fuelSourceItem.m_FireStartSkillModifier = fireStartSkill;

            fuelSourceItem.m_HeatIncrease = heatIncrease;
            fuelSourceItem.m_HeatInnerRadius = heatInner;
            fuelSourceItem.m_HeatOuterRadius = heatOuter;

            fuelSourceItem.m_IsBurntInFireTracked = isBurnt;
            fuelSourceItem.m_IsTinder = isTinder;
            fuelSourceItem.m_IsWet = isWet;
        }
    }
}
