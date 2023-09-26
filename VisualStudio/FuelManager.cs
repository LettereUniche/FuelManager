namespace FuelManager
{
    internal class FuelManager : MelonMod
    {
        public override void OnInitializeMelon()
        {
            Settings.OnLoad(false);
            Spawns.AddToModComponent();
            ConsoleCommands.RegisterCommands();
        }
    }
}