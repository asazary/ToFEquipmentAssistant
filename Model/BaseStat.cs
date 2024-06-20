using System.ComponentModel.DataAnnotations.Schema;
using TofEA.Model;

namespace ToFEA.Model
{
    [Table("BaseStats")]
    public class BaseStat
    {
        public int Id { get; set; }
        public EquipmentType Slot { get; set; } = new();
        public PossibleStat Stat { get; set; } = new();
        public int Value1 { get; set; }
        public int Value2 { get; set; }
        public int Value3 { get; set; }

        public List<Equipment> Equipments { get; set; } = new();
    }
}
