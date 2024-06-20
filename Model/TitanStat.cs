using System.ComponentModel.DataAnnotations.Schema;

namespace ToFEA.Model
{
    [Table("TitanStats")]
    public class TitanStat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public PossibleTitanStat CurrentTitanStat { get; set; } = null!;
        public Equipment Equipment { get; set; } = null!;
        public int StatLevel { get; set; } = 0;
    }
}
