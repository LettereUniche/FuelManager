namespace FuelManager.Utils
{
    using System;
    using System.Reflection;
    using Il2Cpp;
    using Il2CppTLD.Gear;
    using MelonLoader;
    using HarmonyLib;
    using ModSettings;
    using ModComponent;
    using UnityEngine;
    internal class Fuel
    {
        // Panels
        internal static Panel_Inventory_Examine _Panel_Inventory_Examine    = new();
        internal static Panel_OptionsMenu _Panel_OptionsMenu                = new();
        internal static Panel_GenericProgressBar _Panel_GenericProgressBar  = new();

        internal static LiquidItem _LiquidItem                              = new();
        //KeroseneLampItem _KeroseneLampItem                                  = _Panel_Inventory_Examine.m_GearItem.GetComponent<KeroseneLampItem>();

        public const float MIN_LITERS                                       = 0.001f;
        private const string REFUEL_AUDIO                                   = "Play_SndActionRefuelLantern";
        internal static readonly float REFUEL_TIME                          = Settings._settings.refuelTime;
        //private const float REFUEL_TIME                                     = 3f;

        #region Add

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gearItem"></param>
        /// <param name="liters"></param>
        private static void AddLiters(GearItem gearItem, float liters)
        {
            if (gearItem == null) return;
            if (liters == 0) return;

            if (IsKeroseneLamp(gearItem))
            {
                gearItem.m_KeroseneLampItem.m_CurrentFuelLiters += liters;
                //gearItem.m_KeroseneLampItem.m_CurrentFuelLiters = Mathf.Clamp(gearItem.m_KeroseneLampItem.m_CurrentFuelLiters, MIN_LITERS, gearItem.m_KeroseneLampItem.m_MaxFuelLiters);
            }
            else if (IsFuelContainer(gearItem))
            {
                gearItem.m_LiquidItem.m_LiquidLiters += liters;
            }
        }

        private static void AddTotalCurrentLiters(float liters, GearItem excludeItem)
        {
            float remaining = liters;

            foreach (GameObject eachItem in GameManager.GetInventoryComponent().m_Items)
            {
                GearItem gearItem = eachItem.GetComponent<GearItem>();
                if (gearItem == null || gearItem == excludeItem) continue;

                LiquidItem liquidItem = gearItem.m_LiquidItem;
                if (liquidItem == null || liquidItem.m_LiquidType != GearLiquidTypeEnum.Kerosene) continue;

                float previousLiters = liquidItem.m_LiquidLiters;
                liquidItem.m_LiquidLiters = Mathf.Clamp(liquidItem.m_LiquidLiters + remaining, 0, liquidItem.m_LiquidCapacityLiters);
                float transferred = liquidItem.m_LiquidLiters - previousLiters;

                remaining -= transferred;

                if (Mathf.Abs(remaining) < MIN_LITERS) break;
            }
        }

        #endregion

        #region Is

        /// <summary>
        /// Is the gear item purely a container for kerosene?
        /// </summary>
        /// <returns>True if gearItem.m_LiquidItem is not null and is for kerosene.</returns>
        internal static bool IsFuelContainer(GearItem gearItem)
        {
            if (gearItem == null) return false;
            if (!gearItem.m_LiquidItem) return false;
            return gearItem.m_LiquidItem.m_LiquidType == GearLiquidTypeEnum.Kerosene;
        }

        /// <summary>
        /// Is the gear item a kerosene lamp?
        /// </summary>
        /// <returns>True if gearItem.m_KeroseneLampItem is not null.</returns>
        internal static bool IsKeroseneLamp(GearItem gearItem)
        {
            return gearItem.m_KeroseneLampItem != null;
        }

        /// <summary>
        /// Can the gear item hold kerosene?
        /// </summary>
        /// <returns>True if the gear item is a fuel container or is a kerosene lamp.</returns>
        internal static bool IsFuelItem(GearItem gearItem)
        {
            return IsFuelContainer(gearItem) || IsKeroseneLamp(gearItem);
        }
        #endregion
        #region new
        // NEW

        /// <summary>
        /// Gets currently available liters of fuel
        /// </summary>
        /// <returns></returns>
        internal static float GetInventoryLiters()
        {
            return GameManager.GetPlayerManagerComponent().GetTotalLiters(GearLiquidTypeEnum.Kerosene);
        }

        /// <summary>
        /// Gets the amount of drained fuel
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="gearLiquidTypeEnum"></param>
        /// <returns></returns>
        internal static float GetInventoryDeductionLiters(float amount, GearLiquidTypeEnum gearLiquidTypeEnum = GearLiquidTypeEnum.Kerosene)
        {
            return GameManager.GetPlayerManagerComponent().DeductLiquidFromInventory(amount, gearLiquidTypeEnum);
        }

        /*
        /// <summary>
        /// Adds the requested fuel to the requested item
        /// </summary>
        /// <param name="gi">The target item</param>
        /// <param name="fuelLiters">the amount to add</param>
        /// <returns>Total added to item, if 0f nothing was added</returns>
        internal static float AddFuel(GearItem gi, float fuelLiters)
        {
            if (gi && fuelLiters > MIN_LITERS)
            {
                if (IsKeroseneLamp(gi))
                {
                    KeroseneLampItem lamp = gi.GetComponent<KeroseneLampItem>();                                    // instintate lamp
                    float maxFuelToAdd = lamp.GetMaxFuelToAdd();                                                    // get lamp max capacity
                    float num = Mathf.Min(fuelLiters, maxFuelToAdd);                                                // ?

                    lamp.m_CurrentFuelLiters += num;                                                                // add num to current liters
                    lamp.m_CurrentFuelLiters = Mathf.Clamp(lamp.m_CurrentFuelLiters, 0f, lamp.m_MaxFuelLiters);     // ensure current liters is not over max liters

                    return fuelLiters - num;
                }
                else if (IsFuelItem(gi))
                {
                    GearItem gearItem = gi.GetComponent<GearItem>();
                    LiquidItem liquidItem = gearItem.m_LiquidItem;

                    float currentLiters = liquidItem.m_LiquidLiters;
                    float maxFuelToAdd = liquidItem.m_LiquidCapacityLiters - currentLiters;
                    float num = Mathf.Min(fuelLiters, maxFuelToAdd);

                    liquidItem.m_LiquidLiters += num;
                    liquidItem.m_LiquidLiters = Mathf.Clamp(currentLiters, 0f, maxFuelToAdd);

                    return fuelLiters - num;
                }
            }
            return 0f;
        }
        */
        #endregion
        // OLD
        #region Get

        /// <summary>
        /// Returns a liquid quantity string with respect to the game units;
        /// </summary>
        internal static string GetLiquidQuantityString(float quantityLiters)
        {
            return Utils.GetLiquidQuantityString(_Panel_OptionsMenu.State.m_Units, quantityLiters);
        }

        /// <summary>
        /// Returns a liquid quantity string with respect to the game units;
        /// </summary>
        internal static string GetLiquidQuantityStringNoOunces(float quantityLiters)
        {
            return Utils.GetLiquidQuantityStringNoOunces(_Panel_OptionsMenu.State.m_Units, quantityLiters);
        }

        /// <summary>
        /// Returns a liquid quantity string with respect to the game units;
        /// </summary>
        internal static string GetLiquidQuantityStringWithUnits(float quantityLiters)
        {
            return Utils.GetLiquidQuantityStringWithUnits(_Panel_OptionsMenu.State.m_Units, quantityLiters);
        }

        /// <summary>
        /// Returns a liquid quantity string with respect to the game units;
        /// </summary>
        internal static string GetLiquidQuantityStringWithUnitsNoOunces(float quantityLiters)
        {
            return Utils.GetLiquidQuantityStringWithUnitsNoOunces(_Panel_OptionsMenu.State.m_Units, quantityLiters);
        }

        /// <summary>
        /// Returns a liquid quantity string with respect to the game units;
        /// </summary>
        internal static float GetLitersToDrain(GearItem gearItem)
        {
            return Mathf.Min(
                        GetIndividualCurrentLiters(gearItem),  //available fuel
                        GetTotalSpaceLiters(gearItem)          //available capacity
                        );
        }

        internal static float GetLitersToRefuel(GearItem gearItem)
        {
            return Mathf.Min(
                        GetIndividualSpaceLiters(gearItem),     //amount of space in the fuel container
                        GetTotalCurrentLiters(gearItem)         //current amount of kerosene in other containers
                        );
        }

        /// <summary>
        /// Returns the current capacity (in liters) of kerosene for the gear item.
        /// </summary>
        internal static float GetIndividualCapacityLiters(GearItem gearItem)
        {
            if (IsFuelContainer(gearItem)) return gearItem.m_LiquidItem.m_LiquidCapacityLiters;
            else if (IsKeroseneLamp(gearItem)) return gearItem.m_KeroseneLampItem.m_MaxFuelLiters;
            else return 0;
        }

        /// <summary>
        /// Returns the current amount (in liters) of kerosene in the gear item.
        /// </summary>
        internal static float GetIndividualCurrentLiters(GearItem gearItem)
        {
            if (IsFuelContainer(gearItem)) return gearItem.m_LiquidItem.m_LiquidLiters;
            else if (IsKeroseneLamp(gearItem)) return gearItem.m_KeroseneLampItem.m_CurrentFuelLiters;
            else return 0;
        }

        /// <summary>
        /// Get the amount of space in the fuel container.
        /// </summary>
        /// <param name="gearItem">The fuel container being investigated.</param>
        /// <returns>The amount (in liters) of empty space in the fuel container.</returns>
        internal static float GetIndividualSpaceLiters(GearItem gearItem)
        {
            return GetIndividualCapacityLiters(gearItem) - GetIndividualCurrentLiters(gearItem);
        }

        /// <summary>
        /// Get the total capacity of all other fuel containers in the inventory.
        /// </summary>
        /// <param name="excludeItem">The gear item to be excluded from the calculations.</param>
        /// <returns>The total capacity (in liters) from inventory fuel containers.</returns>
        internal static float GetTotalCapacityLiters(GearItem excludeItem)
        {
            float result = 0;

            foreach (GameObject eachItem in GameManager.GetInventoryComponent().m_Items)
            {
                GearItem? gearItem = eachItem?.GetComponent<GearItem>();
                if (gearItem == null || gearItem == excludeItem || !IsFuelContainer(gearItem))
                {
                    continue;
                }

                result += GetIndividualCapacityLiters(gearItem);
            }

            return result;
        }

        /// <summary>
        /// Get the total kerosene quantity of all other fuel containers in the inventory.
        /// </summary>
        /// <param name="excludeItem">The gear item to be excluded from the calculations.</param>
        /// <returns>The total kerosene quantity (in liters) from other inventory fuel containers.</returns>
        internal static float GetTotalCurrentLiters(GearItem excludeItem)
        {
            float result = 0;

            foreach (GameObject eachItem in GameManager.GetInventoryComponent().m_Items)
            {
                GearItem gearItem = eachItem.GetComponent<GearItem>();
                if (gearItem == null || gearItem == excludeItem || !IsFuelContainer(gearItem)) continue;

                result += GetIndividualCurrentLiters(gearItem);
            }

            return result;
        }

        /// <summary>
        /// Get the total empty space of all other fuel containers in the inventory.
        /// </summary>
        /// <param name="excludeItem">The gear item to be excluded from the calculations.</param>
        /// <returns>The total empty space (in liters) from other inventory fuel containers.</returns>
        internal static float GetTotalSpaceLiters(GearItem excludeItem)
        {
            float result = 0;

            foreach (GameObject eachItem in GameManager.GetInventoryComponent().m_Items)
            {
                GearItem gearItem = eachItem.GetComponent<GearItem>();
                if (gearItem != null && gearItem != excludeItem && IsFuelContainer(gearItem))
                {
                    result += GetIndividualSpaceLiters(gearItem);
                }
            }

            return result;
        }

        #endregion
        #region Actions

        internal static void Drain(GearItem gearItem)
        {
#if DEBUG
            FuelManager.Log($"[FuelManager] Drain Start");
#endif
            Panel_Inventory_Examine panel = InterfaceManager.GetPanel<Panel_Inventory_Examine>();

            float currentLiters     = GetIndividualCurrentLiters(panel.m_GearItem.GetComponent<GearItem>());
            float totalCapacity     = GetTotalCapacityLiters(panel.m_GearItem.GetComponent<GearItem>());
            float totalCurrent      = GetTotalCurrentLiters(panel.m_GearItem.GetComponent<GearItem>());

#if DEBUG
            FuelManager.Log($"[FuelManager] currentLiters: {currentLiters}, totalCurrent {totalCurrent}, totalCapacity: {totalCapacity}");
            FuelManager.Log($"[FuelManager] item is {gearItem.name}, GetComponent is {gearItem.GetComponent<GearItem>().name}");
            FuelManager.Log($"[FuelManager] panel item is {panel.m_GearItem.name}, panel item GetComponent is {panel.m_GearItem.GetComponent<GearItem>().name}");
#endif

            if (currentLiters < MIN_LITERS)
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_BFM_AlreadyEmpty"));
                GameAudioManager.PlayGUIError();
#if DEBUG
                FuelManager.LogWarning($"[FuelManager] Already Empty");
                FuelManager.LogWarning($"[FuelManager] Drain End");
#endif
                return;
            }

            if (Mathf.Approximately(totalCapacity, totalCurrent))
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_BFM_NoFuelCapacityAvailable"));
                GameAudioManager.PlayGUIError();
#if DEBUG
                FuelManager.LogWarning($"[FuelManager] No Capacity");
                FuelManager.LogWarning($"[FuelManager] Drain End");
#endif
                return;
            }

            GameAudioManager.PlayGuiConfirm();
            InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(
                Localization.Get("GAMEPLAY_BFM_DrainingProgress"),
                REFUEL_TIME,
                0,
                0,
                REFUEL_AUDIO,
                null,
                false,
                true,
                new System.Action<bool, bool, float>(OnDrainFinished));

            // HACK: somehow this is needed to revert the button text to "Refuel", which will be active when draining finishes
            ButtonUtils.SetButtonLocalizationKey(panel.m_RefuelPanel.GetComponentInChildren<UIButton>(), "GAMEPLAY_Refuel");
#if DEBUG
            FuelManager.Log($"[FuelManager] Drain End");
#endif
        }

        internal static void Refuel(GearItem gearItem)
        {
#if DEBUG
            FuelManager.Log($"[FuelManager] Refuel Start");
#endif

            Panel_Inventory_Examine panel = InterfaceManager.GetPanel<Panel_Inventory_Examine>();

            if (!panel.m_GearItem.GetComponent<GearItem>())
            {
                GameAudioManager.PlayGUIError();
#if DEBUG
                MelonLogger.Msg($"[FuelManager] Gear is not valid");
                MelonLogger.Msg($"[FuelManager] Refuel End");
#endif
                return;
            }

            //GearItem _GEARITEM = _Panel_Inventory_Examine.m_GearItem;

            float currentLiters             = GetIndividualCurrentLiters(panel.m_GearItem.GetComponent<GearItem>());
            float capacityLiters            = GetIndividualCapacityLiters(panel.m_GearItem.GetComponent<GearItem>());
            float totalCurrent              = GetTotalCurrentLiters(panel.m_GearItem.GetComponent<GearItem>());

            float totalInventoryFuel        = GameManager.GetPlayerManagerComponent().GetCapacityLiters(GearLiquidTypeEnum.Kerosene);
            float totalInventoryCapacity    = GameManager.GetPlayerManagerComponent().GetTotalLiters(GearLiquidTypeEnum.Kerosene);
#if DEBUG
            FuelManager.Log($"[FuelManager] currentLiters: {currentLiters}, capacityLiters: {capacityLiters}, totalCurrent: {totalCurrent}");
            FuelManager.Log($"[FuelManager] item is {gearItem.name}, GetComponent is {gearItem.GetComponent<GearItem>().name}");
            FuelManager.Log($"[FuelManager] panel item is {panel.m_GearItem.name}, panel item GetComponent is {panel.m_GearItem.GetComponent<GearItem>().name}");
#endif
            if (Mathf.Approximately(currentLiters, capacityLiters))
            {
                GameAudioManager.PlayGUIError();
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_BFM_AlreadyFilled"), true, true);
#if DEBUG
                FuelManager.LogWarning($"[FuelManager] Already filled");
                FuelManager.LogWarning($"[FuelManager] Refuel End");
#endif
                return;
            }

            if (totalCurrent < MIN_LITERS)
            {
                GameAudioManager.PlayGUIError();
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_NoKeroseneavailable"), false);
#if DEBUG
                FuelManager.LogWarning($"[FuelManager] No available fuel");
                FuelManager.LogWarning($"[FuelManager] Refuel End");
#endif
                return;
            }

            GameAudioManager.PlayGuiConfirm();
            InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(
                Localization.Get("GAMEPLAY_RefuelingProgress"),
                REFUEL_TIME,
                0,
                0,
                REFUEL_AUDIO,
                null,
                false,
                true,
                new System.Action<bool, bool, float>(OnRefuelFinished));
#if DEBUG
            FuelManager.Log($"[FuelManager] Refuel End");
#endif
        }

        #endregion
        #region OnActions

        private static void OnDrainFinished(bool success, bool playerCancel, float progress)
        {
#if DEBUG
            FuelManager.Log($"[FuelManager] OnDrainFinished({success}, {playerCancel}, {progress})");
#endif
            Panel_Inventory_Examine panel = InterfaceManager.GetPanel<Panel_Inventory_Examine>();
            //GearItem _GEARITEM = _Panel_Inventory_Examine.m_GearItem;

            if (panel.m_GearItem != null && IsFuelItem(panel.m_GearItem))
            {
                float litersToDrain = GetLitersToDrain(panel.m_GearItem) * progress;
                //float fuelLiters = GameManager.GetPlayerManagerComponent().DeductLiquidFromInventory(litersToDrain, GearLiquidTypeEnum.Kerosene);

                AddTotalCurrentLiters(litersToDrain, panel.m_GearItem); // replaced with above line
                AddLiters(panel.m_GearItem, -litersToDrain); // keep the base reference for the GearItem as AddLiters deals with that
#if DEBUG
                FuelManager.Log($"[FuelManager] item is {panel.m_GearItem.name}, itersToDrain:{litersToDrain}");
#endif
            }

            panel.RefreshMainWindow();
        }

        private static void OnRefuelFinished(bool success, bool playerCancel, float progress)
        {
#if DEBUG
            FuelManager.Log($"[FuelManager] OnRefuelFinished(success:{success}, playerCancel:{playerCancel}, progress:{progress})");
#endif
            Panel_Inventory_Examine panel = InterfaceManager.GetPanel<Panel_Inventory_Examine>();

            if (IsFuelItem(panel.m_GearItem))
            {
                float litersToTransfer = GetLitersToRefuel(panel.m_GearItem) * progress;

                AddTotalCurrentLiters(-litersToTransfer, panel.m_GearItem);
                AddLiters(panel.m_GearItem, litersToTransfer);
#if DEBUG
                FuelManager.Log($"[FuelManager] item is {panel.m_GearItem.name}, litersToTransfer:{litersToTransfer}");
#endif
            }

            panel.RefreshMainWindow();
        }

        #endregion
    }   
}
