namespace FuelManager
{
    using GearSpawner;
    internal static class Spawns
    {
        internal static void AddToModComponent()
        {
            SpawnTagManager.AddFunction("FuelManager", GetProbability);
        }

        private static float GetProbability(DifficultyLevel difficultyLevel, FirearmAvailability firearmAvailability, GearSpawnInfo gearSpawnInfo)
        {
            if (gearSpawnInfo.PrefabName != "GEAR_GasCan" || gearSpawnInfo.PrefabName != "GEAR_GasCanFull") return 0f;
            return difficultyLevel switch
            {
                DifficultyLevel.Pilgram => Settings.Instance.pilgramSpawnExpectation,
                DifficultyLevel.Voyager => Settings.Instance.voyagerSpawnExpectation,
                DifficultyLevel.Stalker => Settings.Instance.stalkerSpawnExpectation,
                DifficultyLevel.Interloper => Settings.Instance.interloperSpawnExpectation,
                DifficultyLevel.Challenge => Settings.Instance.challengeSpawnExpectation,
                _ => 0f,
            };
        }
    }
}
