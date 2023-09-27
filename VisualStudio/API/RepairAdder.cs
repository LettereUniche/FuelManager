using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace FuelManager.API
{
    [RegisterTypeInIl2Cpp]
    public class RepairAdder : MonoBehaviour
    {
        public RepairAdder(IntPtr intPtr) : base(intPtr) { }

        public Il2CppReferenceArray<GearItem> RequiredGear { get; init; }
        public Il2CppStructArray<int> RequiredGearUnits { get; init; }
        public float ConditionIncrease { get; init; }
        public float RepairConditionCap { get; init; }
        public int Duration { get; init; }
        public string Audio { get; init; }
        public Il2CppReferenceArray<ToolsItem> RequiredTools { get; init; }
        public bool RequiresTools { get; init; }
        public bool NeverFail { get; init; }

    }
}
