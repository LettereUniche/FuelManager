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
                DifficultyLevel.Pilgram => Settings._settings.pilgramSpawnExpectation,
                DifficultyLevel.Voyager => Settings._settings.voyagerSpawnExpectation,
                DifficultyLevel.Stalker => Settings._settings.stalkerSpawnExpectation,
                DifficultyLevel.Interloper => Settings._settings.interloperSpawnExpectation,
                DifficultyLevel.Challenge => Settings._settings.challengeSpawnExpectation,
                _ => 0f,
            };
        }
    }
}
