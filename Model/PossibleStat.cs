using System.ComponentModel.DataAnnotations.Schema;
using TofEA.Model;

namespace ToFEA.Model
{
    [Table("PossibleStats")]
    public class PossibleStat
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsPercent { get; set; } = false;
        public double InitValue { get; set; }
        public double MinUpgradeValue { get; set; }
        public double MaxUpgradeValue { get; set; }

        public List<EquipmentType> EquipmentType { get; set; } = new();
        [NotMapped]
        public List<StatLink> StatLinks { get; set; } = new();

    }
}
