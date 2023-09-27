namespace FuelManager
{
    [RegisterTypeInIl2Cpp]
    public class FuelItemAPI : MonoBehaviour
    {
        public static void RefreshRepairComponent(GearItem? gi)
        {
            if (gi is null)
            {
                Logger.LogWarning($"Requested GearItem is null");
                return;
            }

            Repairable? repair = gi.gameObject.GetComponent<Repairable>();

            if (repair is null)
            {
                Logger.LogWarning($"Requested GearItem {gi.name} does not have the Repairable Component");
                return;
            }
            else
            {
                repair = null;
            }
        }

        public static void AddRepair(
            GearItem? gi,
            string[] requiredGear,
            int[] repairUnits,
            string[] extra,
            string audio,
            int duration = 15,
            float ConditionIncrease = 50,
            bool requiresTools = true,
            bool NeverFail = true)
        {
            if (gi is null) return;

            GameObject? Target = GetInstancedObject(gi);

            if (Target is null) return;

            if (Target.GetComponent<Repairable>() is not null) return;

            Repairable repairable = ItemUtils.GetOrCreateComponent<Repairable>(Target);

            try
            {
                repairable.m_RepairAudio            = audio;
                repairable.m_DurationMinutes        = duration;
                repairable.m_ConditionIncrease      = ConditionIncrease;

                repairable.m_RequiredGear           = ItemUtils.GetItems<GearItem>(requiredGear);
                repairable.m_RequiredGearUnits      = repairUnits;

                repairable.m_RepairToolChoices      = ItemUtils.GetItems<ToolsItem>(extra);
                repairable.m_RequiresToolToRepair   = requiresTools;
                repairable.m_NeverFail              = NeverFail;
            }
            catch (Exception e)
            {
                Logger.LogError("Error attempting to add Repairable Component");
                Logger.Log($"Reason: {e}");
            }
        }

        public static void RefreshHarvestComponent(GearItem? gi)
        {
            if (gi is null)
            {
                Logger.LogWarning($"Requested GearItem is null");
                return;
            }

            Harvest? harvest = gi.gameObject.GetComponent<Harvest>();

            if (harvest is null)
            {
                Logger.LogWarning($"Requested GearItem {gi.name} does not have the Repairable Component");
                return;
            }
            else
            {
                harvest = null;
            }
        }

        public static void AddHarvest(
            GearItem gi,
            string[] YieldGear,
            int[] YieldUnits,
            string[] RequiredTools,
            string audio,
            SkillType skillType = SkillType.None,
            int duration = 15
            )
        {
            if (gi is null) return;

            GameObject? Target = GetInstancedObject(gi);

            if (Target is null) return;

            if (Target.GetComponent<Harvest>() is not null) return;

            Harvest harvest = ItemUtils.GetOrCreateComponent<Harvest>(Target);

            try
            {
                harvest.m_Audio = audio;
                harvest.m_DurationMinutes = duration;

                harvest.m_YieldGear = ItemUtils.GetItems<GearItem>(YieldGear);
                harvest.m_YieldGearUnits = YieldUnits;

                harvest.m_AppliedSkillType = skillType;
                harvest.m_RequiredTools = ItemUtils.GetItems<ToolsItem>(RequiredTools);

                harvest.m_GunpowderYield = 0f;
            }
            catch (Exception e)
            {
                Logger.LogError("Error while attempting to add Harvest Component");
                Logger.Log($"Reason: {e}");
            }

        }

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
            if (gi is null) return;

            GameObject? Target = GetInstancedObject(gi);

            if (Target is null) return;

            FuelSourceItem fuelSourceItem                   = ItemUtils.GetOrCreateComponent<FuelSourceItem>(Target);

            try
            {
                fuelSourceItem.m_IsBurntInFireTracked           = isBurnt;
                fuelSourceItem.m_IsWet                          = isWet;
                fuelSourceItem.m_IsTinder                       = isTinder;

                fuelSourceItem.m_FireAgeMinutesBeforeAdding     = fireAge;

                fuelSourceItem.m_HeatOuterRadius                = heatOuter;
                fuelSourceItem.m_HeatInnerRadius                = heatInner;
                fuelSourceItem.m_HeatIncrease                   = heatIncrease;

                fuelSourceItem.m_BurnDurationHours              = burnHours;
                fuelSourceItem.m_FireStartDurationModifier      = fireStartDuration;
                fuelSourceItem.m_FireStartSkillModifier         = fireStartSkill;
            }
            catch (Exception e)
            {
                Logger.LogError($"Could not add FuelSourceItem to {gi.name}");
                Logger.Log($"Reason: {e}");
            }
        }

        public static GameObject? GetInstancedObject(GearItem gi)
        {
            GameObject? Target      = null;
            GameObject? gameObject  = null;
            GearItem? prefab        = null;

            if (gi is null) return null;

            try
            {
                gameObject = GameObject.Find(gi.name);
            }
            catch (Exception e)
            {
#if DEBUG
                Logger.LogSeperator();
                Logger.LogError($"Could not Instantiate item {gi.name}");
                Logger.Log($"Reason: {e}");
                Logger.LogSeperator();
#endif
            }

            try
            {
                if (gameObject != null)
                {
                    GearItem component = gameObject.GetComponent<GearItem>();
                    prefab = ItemUtils.GetGearItemPrefab(ItemUtils.NormalizeName(component.name)!);
                    Target = prefab.gameObject;
                }
                else
                {
                    prefab = ItemUtils.GetGearItemPrefab(ItemUtils.NormalizeName(gi.name)!);
                    Target = prefab.gameObject;
                }
            }
            catch (Exception e)
            {
#if DEBUG
                Logger.LogSeperator();
                Logger.LogError("Attempting to get the Prefab failed");
                Logger.Log($"GearItem: {gi.name}");
                Logger.Log($"Reason: {e}");
                Logger.LogSeperator();
#endif

                Target = gi.gameObject;
            }

            return Target;
        }
    }
}
