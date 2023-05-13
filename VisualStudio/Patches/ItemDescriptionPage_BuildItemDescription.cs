//namespace FuelManager.Patches
//{
//    [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.BuildItemDescription), new Type[] { typeof(GearItem) } )]
//    internal class ItemDescriptionPage_BuildItemDescription
//    {
//        private static void PostFix(GearItem gi)
//        {
//            if (FuelUtils.IsFuelContainer(gi))
//            {
//                ItemUtils.SetConditionToMax(gi);
//            }
//        }
//    }
//}
