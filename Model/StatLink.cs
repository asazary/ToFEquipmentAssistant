using System.ComponentModel.DataAnnotations.Schema;

namespace ToFEA.Model
{
    [Table("StatLinks")]
    public class StatLink
    {
        public int Id { get; set; }
        public PossibleStat ElemStat { get; set; } = new();
        public PossibleStat CommonStat { get; set; } = new();
        public PossibleStat? PercentStat { get; set; } = new();
        public PossibleStat? DamageStat { get; set; } = new();
    }
}
