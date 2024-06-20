using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ToFEA.BestSetPage
{
    /// <summary>
    /// Interaction logic for BestSetPage.xaml
    /// </summary>
    public partial class BestSetPage : Page
    {
        private int BaseAttack = 20000;
        private readonly int BaseRes = 7000;

        private List<Variant> Variants = [];


        public BestSetPage()
        {
            InitializeComponent();

            InitFilterElements();
        }

        private void InitFilterElements()
        {
            MainStat.ItemsSource = TofData.PossibleStats
                .Select(s => new Extra.EqAdd_Stat_Item
                {
                    Id = s.Id,
                    Name = s.Name,
                    Parent = MainStat,
                    Stat = s
                });
            var secondStats = new List<Extra.EqAdd_Stat_Item>
            {
                new() {
                    Id = 0,
                    Name = "-- None --"
                }                
            };
            secondStats.AddRange(TofData.PossibleStats
                .Select(s => new Extra.EqAdd_Stat_Item
                {
                    Id = s.Id,
                    Name = s.Name,
                    Parent = SecondStat,
                    Stat = s
                }));
            SecondStat.ItemsSource = secondStats;
            MainStat.SelectedIndex = 0;
            SecondStat.SelectedIndex = 0;
        }


        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9\\.]+");
        }

        internal void EqSetGo_Click(object sender, RoutedEventArgs e)
        {
            BaseAttack = int.Parse(BaseAttackValue.Text.Trim());
            var mainStatId = ((Extra.EqAdd_Stat_Item)MainStat.SelectedItem).Id;
            var secondStatId = ((Extra.EqAdd_Stat_Item)SecondStat.SelectedItem).Id;

            Variants = BestSetActions.CalculateVariants(BaseAttack, BaseRes, mainStatId, secondStatId);

            VariantsTable.ItemsSource = Variants.Select(v => new VariantForTable(v, mainStatId, secondStatId)).ToList();
            
        }

        internal void Variants_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                var curVar = (item.Content as VariantForTable)!;
                var variant = Variants.First(v => v.Id == curVar.Id);

                ShowVariantEquips(variant);
                ShowVariantInfo(variant);
            }
        }

        internal void ShowVariantEquips(Variant variant)
        {

            VariantEquips.ItemsSource = 
                variant.Equips.Select(e => new
            {
                Id = e.Id,
                Type = e.EquipmentType.Name,
                Stat1 = $"{e.Stats[0].CurrentStat.Name}: {e.Stats[0].Value}",
                Stat2 = $"{e.Stats[1].CurrentStat.Name}: {e.Stats[1].Value}",
                Stat3 = $"{e.Stats[2].CurrentStat.Name}: {e.Stats[2].Value}",
                Stat4 = $"{e.Stats[3].CurrentStat.Name}: {e.Stats[3].Value}",
            }).ToList();
        }

        internal void ShowVariantInfo(Variant variant)
        {

            var table = variant.StatValues.Join(TofData.PossibleStats,
                s => s.Key, st => st.Id,
                (s, st) => new
                {
                    st.Name,
                    s.Value
                });

            VariantInfo.ItemsSource = table;
        }



        private void VariantInfo_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            //var column = e.AddedCells.First().Column;
            //var 
            //var fc = col.GetCellContent(info.First().Item);
            //MessageBox.Show((fc as TextBlock)?.Text);
            
        }
    }

    public class VariantForTable(Variant variant, int mainStatId, int secStatId)
    {
        public int Id { get; set; } = variant.Id;
        public double MainStat { get; set; } = variant.StatValues[mainStatId];
        public double SecondStat { get; set; } = secStatId > 0 ? variant.StatValues[secStatId] : 0;
    }

    public class VariantEquipsTable
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Stat1 { get; set; } = null!;
        public string Stat2 { get; set; } = null!;
        public string Stat3 { get; set; } = null!;
        public string Stat4 { get; set; } = null!;
    }
}
