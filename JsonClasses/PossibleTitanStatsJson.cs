namespace ToFEA.JsonClasses
{
    public class PossibleTitanStatsJson
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<int> Slots { get; set; } = new();
    }
}
