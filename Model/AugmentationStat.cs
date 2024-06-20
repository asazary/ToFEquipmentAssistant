using System.ComponentModel.DataAnnotations.Schema;

namespace ToFEA.Model
{
    [Table("AugmentationStats")]
    public class AugmentationStat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public PossibleStat CurrentStat { get; set; } = null!;
        public double Value { get; set; }

        public Equipment Equipment { get; set; } = new();
    }
}
