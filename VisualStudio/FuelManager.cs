namespace FuelManager
{
    internal class FuelManager : MelonMod
    {
        public override void OnInitializeMelon()
        {
            Settings.OnLoad();
            Spawns.AddToModComponent();
        }
    }
}