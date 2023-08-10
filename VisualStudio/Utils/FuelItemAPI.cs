namespace FuelManager
{
    public class FuelItemAPI
    {

        /// <summary>
        /// This should only be used to add repair to already existing items. Otherwise use the Modcomponent function
        /// </summary>
        /// <param name="gi"></param>
        public static void AddRepair(GearItem gi)
        {
            AddRepair(gi,
                      "Play_RepairingMetal",
                      15,
                      50,
                      Constants.REPAIR_HARVEST_GEAR,
                      new int[] { 1 },
                      Constants.REPAIR_TOOLS,
                      true,
                      true);
        }

        /// <summary>
        /// This should only be used to add repair to already existing items. Otherwise use the Modcomponent function
        /// </summary>
        /// <param name="gi"></param>
        /// <param name="audio"></param>
        /// <param name="duration"></param>
        /// <param name="ConditionIncrease"></param>
        /// <param name="requiredGear"></param>
        /// <param name="repairUnits"></param>
        /// <param name="extra"></param>
        /// <param name="requiresTools"></param>
        /// <param name="NeverFail"></param>
        public static void AddRepair(GearItem gi,
                                     string audio,
                                     int duration,
                                     float ConditionIncrease,
                                     string[] requiredGear,
                                     int[] repairUnits,
                                     string[] extra,
                                     bool requiresTools,
                                     bool NeverFail)
        {
            if (gi == null) return;
            if (gi.GetComponent<Repairable>() && gi.GetComponent<Repairable>().m_DurationMinutes == duration) return;

            Repairable repairable               = ItemUtils.GetOrCreateComponent<Repairable>(gi.gameObject);

            repairable.m_RepairAudio            = audio;
            repairable.m_DurationMinutes        = duration;
            repairable.m_ConditionIncrease      = ConditionIncrease;

            repairable.m_RequiredGear           = ItemUtils.GetItems<GearItem>(requiredGear);
            repairable.m_RequiredGearUnits      = repairUnits;

            repairable.m_RepairToolChoices      = ItemUtils.GetItems<ToolsItem>(extra);
            repairable.m_RequiresToolToRepair   = requiresTools;
            repairable.m_NeverFail              = NeverFail;
        }

        /// <summary>
        /// This should only be used to add harvest to already existing items. Otherwise use the Modcomponent function
        /// </summary>
        /// <param name="gi">The GearItem you want to add harvest to</param>
        public static void AddHarvest(GearItem gi)
        {
            if (gi == null) return;
            if (gi.GetComponent<Harvest>() && gi.GetComponent<Repairable>().m_DurationMinutes == 15) return;

            AddHarvest(gi,
                       "Play_HarvestingMetalSaw",
                       15,
                       Constants.REPAIR_HARVEST_GEAR,
                       new int[] { 2 },
                       SkillType.None,
                       Constants.HARVEST_TOOLS,
                       0f);
        }

        public static void AddHarvest(GearItem gi, SkillType skillType)
        {
            if (gi == null) return;
            if (gi.GetComponent<Harvest>() && gi.GetComponent<Repairable>().m_DurationMinutes == 15) return;

            AddHarvest(gi,
                       "Play_HarvestingMetalSaw",
                       15,
                       Constants.REPAIR_HARVEST_GEAR,
                       new int[] { 2 },
                       skillType,
                       Constants.HARVEST_TOOLS,
                       0f);
        }


        public static void AddHarvest(GearItem gi, string audio, int duration, string[] yieldGear, int[] yieldUnits, SkillType skillType, string[] requiredTools, float gunpowder)
        {
            if (gi == null) return;
            if (gi.GetComponent<Harvest>() && gi.GetComponent<Repairable>().m_DurationMinutes == duration) return;

            Harvest harvest                 = ItemUtils.GetOrCreateComponent<Harvest>(gi.gameObject);

            harvest.m_Audio                 = audio;
            harvest.m_DurationMinutes       = duration;

            harvest.m_YieldGear             = ItemUtils.GetItems<GearItem>(yieldGear);
            harvest.m_YieldGearUnits        = yieldUnits;

            harvest.m_AppliedSkillType      = skillType;
            harvest.m_RequiredTools         = ItemUtils.GetItems<ToolsItem>(requiredTools);
            harvest.m_GunpowderYield        = gunpowder;
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
            if (gi == null) return;
            if (gi.GetComponent<FuelSourceItem>() && gi.GetComponent<FuelSourceItem>().m_BurnDurationHours == burnHours) return;

            FuelSourceItem fuelSourceItem                   = ItemUtils.GetOrCreateComponent<FuelSourceItem>(gi.gameObject);

            fuelSourceItem.m_BurnDurationHours              = burnHours;

            fuelSourceItem.m_FireAgeMinutesBeforeAdding     = fireAge;
            fuelSourceItem.m_FireStartDurationModifier      = fireStartDuration;
            fuelSourceItem.m_FireStartSkillModifier         = fireStartSkill;

            fuelSourceItem.m_HeatIncrease                   = heatIncrease;
            fuelSourceItem.m_HeatInnerRadius                = heatInner;
            fuelSourceItem.m_HeatOuterRadius                = heatOuter;

            fuelSourceItem.m_IsBurntInFireTracked           = isBurnt;
            fuelSourceItem.m_IsTinder                       = isTinder;
            fuelSourceItem.m_IsWet                          = isWet;
        }
    }
}
