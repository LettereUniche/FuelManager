namespace FuelManager
{
    public class AddRepair : MonoBehaviour
    {
        public void Start()
        {

        }

        public GearItem[]? RequiredGear;
        public int[]? RequiredGearUnits;
        public float? ConditionIncrease;
        public float? RepairConditionCap = 100f;
        public int? DurationMinutes;
        public string? RepairAudio;
        public ToolsItem[]? RepairToolChoices;
        public bool? RequiresToolToRepair;
        public bool? NeverFail;
    }
}
