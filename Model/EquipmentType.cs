using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;
using ToFEA;
using ToFEA.Model;

namespace TofEA.Model
{
    [Table("EquipmentTypes")]
    public class EquipmentType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ShortName { get; set; } = null!;
        public int Generation { get; set; } = 1;
        public string IconName { get; set; } = null!;
        public List<PossibleStat> PossibleStats { get; set; } = new();
        public List<PossibleTitanStat> PossibleTitanStats { get; set; } = new();
        public List<BaseStat> BaseStats { get; set; } = new();

        public BitmapImage GetImage()
        {
            var uri = $"pack://application:,,,/{Assembly.GetExecutingAssembly().GetName().Name};component/{Path.Join(TofData.ImagesPath, IconName)}";
            return new BitmapImage(new Uri(uri));
        }
    }
}
