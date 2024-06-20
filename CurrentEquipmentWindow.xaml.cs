using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToFEA.Model;
using WpfApp1;
using ToFEA.Extra;

namespace ToFEA
{
    /// <summary>
    /// Interaction logic for CurrentEquipmentWindow.xaml
    /// </summary>
    public partial class CurrentEquipmentWindow : Window
    {
        private MainWindow ParentWindow { get; set; }

        public CurrentEquipmentWindow(MainWindow parent)
        {
            InitializeComponent();

            ParentWindow = parent;

            EqAddType.ItemsSource = TofData.EquipmentTypes
                .Select(s => new EqAdd_Type_Item
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToList();

            EqAddType.SelectedIndex = 0;
            EqAddTypeImage.Source = TofData.EquipmentTypes
                .First(s => s.Id == ((EqAdd_Type_Item)EqAddType.SelectedItem).Id).GetImage();
            EqAddType_OnChange(this, new EventArgs());
        }


        private void EqAddType_OnChange(object sender, EventArgs e)
        {
            if (EqAddType.SelectedItem is EqAdd_Type_Item item)
            {
                var currentType = TofData.EquipmentTypes.First(s => s.Id == item.Id);
                EqAddTypeImage.Source = currentType.GetImage();
                foreach (var (statBox, statValue) in 
                    new [] { (EqAddStat1, EqAddStatValue1), (EqAddStat2, EqAddStatValue2),
                        (EqAddStat3, EqAddStatValue3), (EqAddStat4, EqAddStatValue4) })
                {
                    statBox.ItemsSource = currentType
                    .PossibleStats.Select(s => new EqAdd_Stat_Item
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Parent = statBox,
                        ValueElement = statValue,
                        Stat = s
                    }).ToList();

                    statBox.SelectedIndex = 0;
                }

                BaseStats.ItemsSource = currentType.BaseStats
                    .Select(s => new
                    {
                        s.Stat.Name,
                        Value = EqAddAugmentationLevel.SelectedIndex == 0
                            ? s.Value1 : EqAddAugmentationLevel.SelectedIndex == 1 ? s.Value2 : s.Value3
                    }).ToList();

                InitAugmentationItems();
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9\\.]+");
        }

        private void EqStat_OnChange(object sender, EventArgs e)
        {
            if (sender is ComboBox item)
            {
                var element = item.SelectedValue as EqAdd_Stat_Item;
                if (element != null)
                    element.ValueElement!.Text = element.Stat!.InitValue.ToString();
            }            
        }

        private void AugLevel_OnChange(object sender, EventArgs e)
        {
            if (sender is ComboBox item && AugStatsPanel is not null && TitanSkillsPanel is not null)
            {
                InitAugmentationItems();
            }
        }

        private void InitAugmentationItems()
        {
            var eqType = TofData.EquipmentTypes.First(s => s.Id == ((EqAdd_Type_Item)EqAddType.SelectedItem).Id);

            foreach (var (statBox, statValue) in
                new[] { (AugStat1, AugStat1Value), (AugStat2, AugStat2Value) })
            {
                var stats = eqType
                .PossibleStats.Select(s => new EqAdd_Stat_Item
                {
                    Id = s.Id,
                    Name = s.Name,
                    Parent = statBox,
                    ValueElement = statValue,
                    Stat = s
                }).ToList();
                statBox.ItemsSource = stats;

                statBox.SelectedIndex = 0;
                statValue.Text = stats.First().Stat!.InitValue.ToString();
            }

            if (EqAddAugmentationLevel.SelectedIndex >= 1)
            {
                foreach (var (statBox, statValue) in
                new[] { (AugStat1, AugStat1Value), (AugStat2, AugStat2Value) })
                    {
                        statBox.ItemsSource = eqType
                        .PossibleStats.Select(s => new EqAdd_Stat_Item
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Parent = statBox,
                            ValueElement = statValue,
                            Stat = s
                        }).ToList();

                        statBox.SelectedIndex = 0;
                    }
            }

            if (EqAddAugmentationLevel.SelectedIndex == 2)
            {
                var titanStats = new List<EqAdd_TitanSill_Item>()
                {
                    new EqAdd_TitanSill_Item
                    {
                        Id = 0,
                        Name = "None"
                    }
                };
                titanStats.AddRange(eqType.PossibleTitanStats
                    .Select(s => new EqAdd_TitanSill_Item
                    {
                        Id = s.Id,
                        Name = s.Name
                    }));

                foreach (var (skill, level) in new[] { (TitanSkill1, TitanSkill1Level),
                        (TitanSkill2, TitanSkill2Level), (TitanSkill3, TitanSkill3Level) })
                {
                    skill.ItemsSource = titanStats;
                    skill.SelectedIndex = 0;
                    level.Text = "1";
                }
            }

            AugStatsPanel.Visibility = EqAddAugmentationLevel.SelectedIndex == 0 ? Visibility.Hidden : Visibility.Visible;
            TitanSkillsPanel.Visibility = EqAddAugmentationLevel.SelectedIndex == 2 ? Visibility.Visible : Visibility.Hidden;
        }

        private void CancelNewEquipButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveNewEquipButton_Click(object sender, RoutedEventArgs e)
        {
            var stats = new List<Stat>();
            foreach (var (stat, value) in new[] { 
                (EqAddStat1, EqAddStatValue1),
                (EqAddStat2, EqAddStatValue2),
                (EqAddStat3, EqAddStatValue3),
                (EqAddStat4, EqAddStatValue4)
            })
            {
                var newStat = new Stat()
                {
                    CurrentStat = TofData.PossibleStats.First(s => s.Id == ((EqAdd_Stat_Item)(stat.SelectedItem)).Stat!.Id),
                    Value = float.Parse(value.Text)
                };
                stats.Add(newStat);
            }

            var equipType = TofData.EquipmentTypes.First(e => e.Id == ((EqAdd_Type_Item)EqAddType.SelectedItem).Id);

            var augStats = new List<AugmentationStat>();
            if (EqAddAugmentationLevel.SelectedIndex > 0)
            {
                foreach (var (augStat, value) in new[]
                {
                    (AugStat1, AugStat1Value), (AugStat2, AugStat2Value)
                })
                {
                    var newStat = new AugmentationStat()
                    {
                        CurrentStat = TofData.PossibleStats.First(s => s.Id == ((EqAdd_Stat_Item)(augStat.SelectedItem)).Stat.Id),
                        Value = float.Parse(value.Text)
                    };
                    augStats.Add(newStat);
                }
            }

            var titanStats = new List<TitanStat>();
            if (EqAddAugmentationLevel.SelectedIndex > 1)
            {
                foreach (var (titanStat, value) in new[]
                {
                    (TitanSkill1, TitanSkill1Level), (TitanSkill2, TitanSkill2Level), (TitanSkill3, TitanSkill3Level)
                })
                {
                    var titanStatId = ((EqAdd_TitanSill_Item)(titanStat.SelectedItem)).Id;
                    if (titanStatId == 0) continue;

                    var newStat = new TitanStat()
                    {
                        CurrentTitanStat = TofData.PossibleTitanStats.First(s => s.Id == titanStatId),
                        StatLevel = int.Parse(value.Text)
                    };
                    titanStats.Add(newStat);
                }
            }

            var newEquip = new Equipment
            {
                EquipmentType = equipType,
                NumberOfStars = Stars.SelectedIndex + 1,
                AugmentationLevel = EqAddAugmentationLevel.SelectedIndex,
                Stats = stats,
                BaseStats = equipType.BaseStats,
                AugmentationStats = augStats,
                TitanStats = titanStats
            };

            TofData.AddEquipment(newEquip);
            ParentWindow.RefreshEquipments();

            Close();
        }
    }

    
}
