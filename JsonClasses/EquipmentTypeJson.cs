namespace ToFEA.JsonClasses
{
    public class EquipmentTypeJson
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ShortName { get; set; } = null!;
        public int Generation {  get; set; }
        public List<int> PossibleStats { get; set; } = new();
        public List<int> PossibleTitanStats { get; set; } = new();
        public string IconName {  get; set; } = null!;
    }
}
