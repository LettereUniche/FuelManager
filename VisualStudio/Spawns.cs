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
            return difficultyLevel switch
            {

                DifficultyLevel.Pilgram => Settings._settings.pilgramSpawnExpectation / 70f * 100f,
                DifficultyLevel.Voyager => Settings._settings.voyagerSpawnExpectation / 70f * 100f,
                DifficultyLevel.Stalker => Settings._settings.stalkerSpawnExpectation / 70f * 100f,
                DifficultyLevel.Interloper => Settings._settings.interloperSpawnExpectation / 70f * 100f,
                DifficultyLevel.Challenge => Settings._settings.challengeSpawnExpectation / 70f * 100f,
                _ => 0f,
            };
        }
    }
}
