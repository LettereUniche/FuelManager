namespace FuelManager
{
    public class ConsoleCommands
    {
        private static bool GearItem_LampFuel_Harvest           = false;
        private static bool GearItem_LampFuel_Repairable        = false;
        private static bool GearItem_LampFuelFull_Harvest       = false;
        private static bool GearItem_LampFuelFull_Repairable    = false;
        private static bool GearItem_JerryCan_Harvest           = false;
        private static bool GearItem_JerryCan_Repairable        = false;

        public static void PrintDebugInfo()
        {
            if (Settings.GearItems.Count == Settings.GearNames.Count)
            {

            }
        }

        /// <summary>
        /// This prints all the required test info ONLY while in a savegame. You must have in your inventory: GEAR_LampFuel, GEAR_LampFuelFull and GEAR_JerrycanRusty
        /// </summary>
        private void UpdateTest()
        {
            if (GameManager.IsMainMenuActive())
            {
                Logger.LogWarning("Cant print test info while in the Main Menu");
                return;
            }

            if (!GameManager.GetInventoryComponent())
            {
                Logger.LogWarning("Cant print test info as the InventoryComponent is not yet present");
                return;
            }

            Inventory inventory = GameManager.GetInventoryComponent();

            for (int i = 0; i < inventory.m_Items.Count; i++)
            {
                if (inventory.m_Items[i] == null) continue;
                GearItem gearItem = inventory.m_Items[i];
                if (gearItem == null) continue;

                if (ItemUtils.NormalizeName(gearItem.name) == "GEAR_LampFuel")
                {
                    if (gearItem.GetComponent<Harvest>()) GearItem_LampFuel_Harvest = true;
                    if (gearItem.GetComponent<Repairable>()) GearItem_LampFuel_Repairable = true;
                    continue;
                }
                else if (ItemUtils.NormalizeName(gearItem.name) == "GEAR_LampFuelFull")
                {
                    if (gearItem.GetComponent<Harvest>()) GearItem_LampFuelFull_Harvest = true;
                    if (gearItem.GetComponent<Repairable>()) GearItem_LampFuelFull_Repairable = true;
                    continue;
                }
                else if (ItemUtils.NormalizeName(gearItem.name) == "GEAR_JerrycanRusty")
                {
                    if (gearItem.GetComponent<Harvest>()) GearItem_JerryCan_Harvest = true;
                    if (gearItem.GetComponent<Repairable>()) GearItem_JerryCan_Repairable = true;
                    continue;
                }
                else
                {
                    Logger.LogError("Inventory does not contain required items: GEAR_LampFuel, GEAR_LampFuelFull and GEAR_JerrycanRusty");
                    break;
                }
            }

            string[] UpdateTestResults = 
            {
                $"Lamp Fuel Harvest:            {GearItem_LampFuel_Harvest}",
                $"Lamp Fuel Repairable:         {GearItem_LampFuel_Repairable}",
                $"Lamp Fuel Full Harvest:       {GearItem_LampFuelFull_Harvest}",
                $"Lamp Fuel Full Repairable:    {GearItem_LampFuelFull_Repairable}",
                $"Jerry Can Harvest:            {GearItem_JerryCan_Harvest}",
                $"Jerry Can Repairable:         {GearItem_JerryCan_Repairable}"
            };

            Logger.LogUpdate(UpdateTestResults);
        }

        public void RegisterCommands()
        {
            uConsole.RegisterCommand("FM_UpdateTest", new Action(UpdateTest));
            uConsole.RegisterCommand("FM_PrintChangeLogs", new Action(PatchNotes.PrintChangeLog));
        }
    }
}