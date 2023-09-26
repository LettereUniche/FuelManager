namespace FuelManager
{
    internal class FuelManager : MelonMod
    {
        public static GearLiquidTypeEnum GetKerosene()
        {
            try
            {
                return GearLiquidTypeEnum.Kerosene;
            }
            catch (TypeLoadException)
            {
                Logger.LogError("Il2Cpp.GearLiquidTypeEnum was not found, try rebuilding FM");
                throw;
            }
        }

        public override void OnInitializeMelon()
        {
            Settings.OnLoad(false);
            Spawns.AddToModComponent();
            ConsoleCommands.RegisterCommands();
        }
    }
}