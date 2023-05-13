namespace FuelManager.Patches
{
    [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.CanExamine), new Type[] { typeof(GearItem) })]
    internal class ItemDescriptionPage_CanExamine
    {
        private static bool Prefix(GearItem gi, ref bool __result)
        {
            if (FuelUtils.IsFuelItem(gi))
            {
                __result = true;
                return false;
            }
            return true;
        }
    }
}
