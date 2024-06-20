using System.Security.Cryptography.X509Certificates;
using System.Windows.Controls;
using ToFEA.Model;

namespace ToFEA.Extra
{
    public class EqAdd_Type_Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public override string ToString() => Name;
    }

    public class EqAdd_Stat_Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ComboBox? Parent { get; set; } = new();
        public TextBox? ValueElement { get; set; } = new();
        public PossibleStat? Stat { get; set; } = new();

        public override string ToString() => Name;
    }

    public class EqAdd_TitanSill_Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public override string ToString() => Name;
    }

    public class Filter_Values
    {
        public int TypeId { get; set; }
        public int Stat1Id { get; set; }
        public int Stat2Id { get; set; }
    }
}
