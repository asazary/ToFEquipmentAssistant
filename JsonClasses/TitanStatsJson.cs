using ToFEA.Model;

namespace ToFEA.JsonClasses
{
    internal class TitanStatsJson
    {
        public int Id { get; set; }
        public int TitanStatId { get; set; }
        public int StatLevel { get; set; } = 0;
    }
}
