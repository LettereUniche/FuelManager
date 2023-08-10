namespace FuelManager
{
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
    internal class GearItem_Awake_Add
    {
        public static void Postfix(GearItem __instance)
        {
            if (ItemUtils.NormalizeName(__instance.name) == Settings.GearNames[0])
            {
                Settings.GearItems.Add(__instance);
            }
            else if (ItemUtils.NormalizeName(__instance.name) == Settings.GearNames[1])
            {
                Settings.GearItems.Add(__instance);
            }
            else if (ItemUtils.NormalizeName(__instance.name) == Settings.GearNames[2])
            {
                Settings.GearItems.Add(__instance);
            }
            else if (ItemUtils.NormalizeName(__instance.name) == Settings.GearNames[3])
            {
                Settings.GearItems.Add(__instance);
            }
        }
    }
}
