using System.ComponentModel.DataAnnotations.Schema;
using TofEA.Model;

namespace ToFEA.Model
{
    [Table("PossibleTitanStats")]
    public class PossibleTitanStat
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public List<EquipmentType> EquipmentTypes { get; set; } = new();
    }
}
