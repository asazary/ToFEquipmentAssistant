namespace ToFEA.JsonClasses
{
    public class JsonFile
    {
        public List<EquipmentTypeJson> EquipmentType { get; set; } = new();
        public List<PossibleStatsJson> PossibleStats { get; set; } = new();
        public List<BaseStatsJson> BaseStats { get; set; } = new();
        public List<PossibleTitanStatsJson> PossibleTitanStats { get; set; } = new();
        public List<StatLinksJson> StatLinks { get; set; } = new();
    }
}
