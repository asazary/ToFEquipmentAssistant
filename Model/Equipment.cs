using System.ComponentModel.DataAnnotations.Schema;
using TofEA.Model;

namespace ToFEA.Model
{
    public class Equipment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public EquipmentType EquipmentType { get; set; } = null!;
        public int NumberOfStars { get; set; } = 0;
        public int AugmentationLevel { get; set; } = 0; // 0, 1, 2
        public List<Stat> Stats { get; set; } = new();
        public List<BaseStat> BaseStats { get; set; } = new();
        public List<AugmentationStat> AugmentationStats { get; set; } = new();
        public List<TitanStat> TitanStats { get; set; } = new();
    }
}
