namespace FuelManager.Patches
{
    using Il2Cpp;
    using HarmonyLib;
    using UnityEngine;

    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.RefreshRefuelPanel))]
    internal class Panel_Inventory_Examine_RefreshRefuelPanel
    {
        private static bool Prefix(Panel_Inventory_Examine __instance)
        {
            //GearItem gearItem = __instance.m_GearItem.GetComponent<GearItem>();
            //bool IsFuelItem = FuelUtils.IsFuelItem(gearItem);

            if (FuelUtils.IsFuelItem(__instance.m_GearItem) && __instance != null)
            {
                __instance.m_RefuelPanel.SetActive(false);
                __instance.m_Button_Refuel.gameObject.SetActive(true);

                float currentLiters         = FuelUtils.GetIndividualCurrentLiters(__instance.m_GearItem);
                float capacityLiters        = FuelUtils.GetIndividualCapacityLiters(__instance.m_GearItem);
                float totalCurrent          = FuelUtils.GetTotalCurrentLiters(__instance.m_GearItem);
                float totalCapacity         = FuelUtils.GetTotalCapacityLiters(__instance.m_GearItem);

                bool fuelIsAvailable        = totalCurrent > FuelUtils.MIN_LITERS;
                bool flag                   = fuelIsAvailable && !Mathf.Approximately(currentLiters, capacityLiters);

                __instance.m_Refuel_X.gameObject.SetActive(!flag);
                __instance.m_Button_Refuel.gameObject.GetComponent<Panel_Inventory_Examine_MenuItem>().SetDisabled(!flag);

                bool flag2 = Utils.IsGamepadActive();

                __instance.m_RequiresFuelMessage.SetActive(flag && !flag2);

                __instance.m_MouseRefuelButton.SetActive(flag);

                __instance.m_RequiresFuelMessage.SetActive(!flag);

                __instance.m_LanternFuelAmountLabel.text    = $"{FuelUtils.GetLiquidQuantityStringNoOunces(currentLiters)} / {FuelUtils.GetLiquidQuantityStringNoOunces(capacityLiters)}";
                __instance.m_FuelSupplyAmountLabel.text     = $"{FuelUtils.GetLiquidQuantityStringNoOunces(totalCurrent)} / {FuelUtils.GetLiquidQuantityStringNoOunces(totalCapacity)}";

                __instance.UpdateWeightAndConditionLabels();
            }
            return false;
        }
    }
}
