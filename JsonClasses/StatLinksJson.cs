namespace ToFEA.JsonClasses
{
    public class StatLinksJson
    {
        public int Id { get; set; }
        public int ElemStat { get; set; } = new();
        public int CommonStat { get; set; } = new();
        public int? PercentStat { get; set; } = new();
        public int? DamageStat { get; set; } = new();
    }
}
