namespace ToFEA.JsonClasses
{
    public class PossibleStatsJson
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsPercent { get; set; }
        public double InitValue {  get; set; }
        public double MinUpgradeValue {  get; set; }
        public double MaxUpgradeValue { get; set; }

    }
}
