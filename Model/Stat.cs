using System.ComponentModel.DataAnnotations.Schema;
using TofEA.Model;

namespace ToFEA.Model
{
    [Table("Stats")]
    public class Stat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public PossibleStat CurrentStat { get; set; } = null!;
        public double Value { get; set; }

        public Equipment Equipment { get; set; } = new();
    }
}
