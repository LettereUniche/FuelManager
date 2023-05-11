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
        //internal static readonly float REFUEL_TIME                          = Settings._settings.refuelTime;
        internal static readonly float REFUEL_TIME                          = 3f;

        #region Add

        private static void AddLiters(GearItem gearItem, float liters)
        {
            if (IsKeroseneLamp(gearItem))
            {
                KeroseneLampItem lamp = gearItem.GetComponent<KeroseneLampItem>();
                float maxFuelToAdd = lamp.GetMaxFuelToAdd();
                float num = Mathf.Min(liters, maxFuelToAdd);

                lamp.m_CurrentFuelLiters += num;
                lamp.m_CurrentFuelLiters = Mathf.Clamp(lamp.m_CurrentFuelLiters, 0f, lamp.m_MaxFuelLiters);
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
            return gearItem?.m_LiquidItem != null && gearItem.m_LiquidItem.m_LiquidType == GearLiquidTypeEnum.Kerosene;
        }

        /// <summary>
        /// Is the gear item a kerosene lamp?
        /// </summary>
        /// <returns>True if gearItem.m_KeroseneLampItem is not null.</returns>
        internal static bool IsKeroseneLamp(GearItem gearItem)
        {
            return gearItem?.m_KeroseneLampItem != null;
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
                        GetTotalSpaceLiters(gearItem),          //available capacity
                        GetIndividualCurrentLiters(gearItem));  //available fuel
        }

        internal static float GetLitersToRefuel(GearItem gearItem)
        {
            return Mathf.Min(
                GetTotalCurrentLiters(gearItem), //current amount of kerosene in other containers
                GetIndividualSpaceLiters(gearItem)); //amount of space in the fuel container
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
            //_Panel_Inventory_Examine.m_GearItem = gearItem.GetComponent<GearItem>();
            Panel_Inventory_Examine _Panel_Inventory_Examine = new();
            MelonLogger.Msg($"[FuelManager]: Drain Start");

            float currentLiters = GetIndividualCurrentLiters(gearItem.GetComponent<GearItem>());

            MelonLogger.Msg($"[FuelManager]: item is {gearItem.name}, currentLiters: {currentLiters}");

            if (currentLiters < MIN_LITERS)
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_BFM_AlreadyEmpty"));
                GameAudioManager.PlayGUIError();
                MelonLogger.Msg($"[FuelManager]: Already Empty, Drain End");
                return;
            }

            float totalCapacity = GetTotalCapacityLiters(gearItem.GetComponent<GearItem>());
            float totalCurrent = GetTotalCurrentLiters(gearItem.GetComponent<GearItem>());

            MelonLogger.Msg($"[FuelManager]: totalCurrent {totalCurrent}, totalCapacity: {totalCapacity}");

            if (Mathf.Approximately(totalCapacity, totalCurrent))
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_BFM_NoFuelCapacityAvailable"));
                GameAudioManager.PlayGUIError();
                MelonLogger.Msg($"[FuelManager]: No Capacity, Drain End");
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
            ButtonUtils.SetButtonLocalizationKey(_Panel_Inventory_Examine.m_RefuelPanel.GetComponentInChildren<UIButton>(), "GAMEPLAY_Refuel");
            MelonLogger.Msg($"[FuelManager]: Drain End");
        }

        internal static void Refuel(GearItem gearItem)
        {
            //_Panel_Inventory_Examine.m_GearItem = gearItem.GetComponent<GearItem>();
            MelonLogger.Msg($"[FuelManager]: Refuel Start");

            Panel_Inventory_Examine _Panel_Inventory_Examine = new();
            GearItem _GEARITEM = _Panel_Inventory_Examine.m_GearItem.GetComponent<GearItem>();

            if (!_GEARITEM)
            {
                GameAudioManager.PlayGUIError();
                return;
            }

            float currentLiters = GetIndividualCurrentLiters(_GEARITEM);
            float capacityLiters = GetIndividualCapacityLiters(_GEARITEM);

            MelonLogger.Msg($"[FuelManager]: item is {_GEARITEM.name}, currentLiters: {currentLiters}, capacityLiters: {capacityLiters}");

            if (Mathf.Approximately(currentLiters, capacityLiters))
            {
                GameAudioManager.PlayGUIError();
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_BFM_AlreadyFilled"), false, false);
                MelonLogger.Msg($"[FuelManager]: Already filled, Refuel End");
                return;
            }

            float totalCurrent = GetTotalCurrentLiters(_GEARITEM);
            MelonLogger.Msg($"[FuelManager]: totalCurrent: {totalCurrent}");

            if (Utils.IsZero(GameManager.GetPlayerManagerComponent().GetTotalLiters(GearLiquidTypeEnum.Kerosene), MIN_LITERS))
            {
                GameAudioManager.PlayGUIError();
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_NoKeroseneavailable"), false);
                MelonLogger.Msg($"[FuelManager]: No available fuel, Refuel End");
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
            MelonLogger.Msg($"[FuelManager]: Refuel End");
        }

        #endregion
        #region OnActions

        private static void OnDrainFinished(bool success, bool playerCancel, float progress)
        {
            //Panel_Inventory_Examine _Panel_Inventory_Examine = new();

            MelonLogger.Msg($"[FuelManager]: OnDrainFinished({success}, {playerCancel}, {progress})");

            if (IsFuelItem(_Panel_Inventory_Examine.m_GearItem))
            {
                float litersToDrain = GetLitersToDrain(_Panel_Inventory_Examine.m_GearItem) * progress;
                AddTotalCurrentLiters(litersToDrain, _Panel_Inventory_Examine.m_GearItem);
                AddLiters(_Panel_Inventory_Examine.m_GearItem, -litersToDrain);

                MelonLogger.Msg($"[FuelManager]: litersToDrain:{litersToDrain}");
            }

            _Panel_Inventory_Examine.RefreshMainWindow();
        }

        private static void OnRefuelFinished(bool success, bool playerCancel, float progress)
        {
            MelonLogger.Msg($"[FuelManager]: OnRefuelFinished({success}, {playerCancel}, {progress})");
            //Panel_Inventory_Examine _Panel_Inventory_Examine = new();
            GearItem _GEARITEM = _Panel_Inventory_Examine.m_GearItem.GetComponent<GearItem>();

            if (IsFuelItem(_GEARITEM))
            {
                float litersToTransfer = GetLitersToRefuel(_GEARITEM) * progress;
                AddTotalCurrentLiters(-litersToTransfer, _GEARITEM);
                AddLiters(_GEARITEM, litersToTransfer);

                MelonLogger.Msg($"[FuelManager]: litersToTransfer:{litersToTransfer}");
            }

            _Panel_Inventory_Examine.RefreshMainWindow();
        }

        #endregion
    }   
}
