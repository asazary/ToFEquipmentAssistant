using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using ToFEA;
using ToFEA.Model;
using System.IO;
using ToFEA.Extra;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TofData.InitDb(); 
            
            //TofData.InitDbTestData();

            TofData.LoadDataFromDb();

            LoadEquipmentsFromMemory();

            InitFilterComponents();
        }

        public void InitFilterComponents()
        {
            var types = new List<EqAdd_Type_Item>()
            {
                new() {
                    Id = 0,
                    Name = "-- Type --"
                }
            };
            types.AddRange(TofData.EquipmentTypes
            .Select(s => new EqAdd_Type_Item
            {
                Id = s.Id,
                Name = s.Name
            }));

            FilterType.ItemsSource = types;
            FilterType.SelectedIndex = 0;
            // ------

            InitFilterStatComponents();
        }

        public void InitFilterStatComponents()
        {
            var currentTypeId = (FilterType.SelectedItem as EqAdd_Type_Item)!.Id;

            var stats = new List<EqAdd_Stat_Item>()
            {
                new() {
                    Id = 0,
                    Name = "-- Random Stat --"
                }
            };
            if (currentTypeId == 0)
                stats.AddRange(TofData.PossibleStats.Select(s => new EqAdd_Stat_Item
                {
                    Id = s.Id,
                    Name = s.Name
                }));
            else
                stats.AddRange(TofData.EquipmentTypes.First(e => e.Id == currentTypeId)
                    .PossibleStats.Select(s => new EqAdd_Stat_Item
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).ToList());

            FilterStat1.ItemsSource = stats;
            FilterStat2.ItemsSource = stats;
            FilterStat1.SelectedIndex = 0;
            FilterStat2.SelectedIndex = 0;
            
        }


        public void ApplyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshEquipments();
        }

        public void RefreshEquipments()
        {
            var currentTypeId = (FilterType.SelectedItem as EqAdd_Type_Item)!.Id;
            var currentStat1Id = (FilterStat1.SelectedItem as EqAdd_Stat_Item)!.Id;
            var currentStat2Id = (FilterStat2.SelectedItem as EqAdd_Stat_Item)!.Id;

            var filters = new Filter_Values
            {
                TypeId = currentTypeId,
                Stat1Id = currentStat1Id,
                Stat2Id = currentStat2Id
            };

            LoadEquipmentsFromMemory(filters);
        }


        public void LoadEquipmentsFromMemory(Filter_Values? filters = null)
        {
            IEnumerable<Equipment> items = TofData.Equipments;

            if (filters != null)
            {
                if (filters.TypeId > 0)
                    items = items.Where(e => e.EquipmentType.Id == filters.TypeId);

                if (filters.Stat1Id > 0)
                    items = items.Where(e => e.Stats.Select(s => s.CurrentStat.Id).Contains(filters.Stat1Id));

                if (filters.Stat2Id > 0)
                    items = items.Where(e => e.Stats.Select(s => s.CurrentStat.Id).Contains(filters.Stat2Id));
            }

            Equipments.ItemsSource = items.OrderByDescending(e => e.Id)
                .Select(e => new EquipmentForTable(e))
                .ToList();
        }

        internal void ShowEquipmentInfo(Equipment item)
        {
            EqTypeName.Text = item.EquipmentType.Name;

            var uri = $"pack://application:,,,/{Assembly.GetExecutingAssembly().GetName().Name};component/{Path.Join(TofData.ImagesPath, item.EquipmentType.IconName)}";
            EqImg.Source = new BitmapImage(new Uri(uri));

            var baseStats = item.BaseStats.Select(e => new
            {
                Name = e.Stat.Name,
                Value = item.AugmentationLevel == 1 ? e.Value2 : item.AugmentationLevel == 2 ? e.Value3 : e.Value1
            }).ToList();

            var randomStats = item.Stats.Select(e => new
            {
                e.CurrentStat.Name,
                e.Value
            }).ToList();

            var augmentationStats = item.AugmentationStats.Select(e => new
            {
                e.CurrentStat.Name,
                e.Value
            }).ToList();

            var titanSkills = item.TitanStats.Select(e => new
            {
                e.CurrentTitanStat.Name,
                e.StatLevel
            });

            EqBaseStats.FrozenColumnCount = 2;
            EqRandomStats.FrozenColumnCount = 2;

            EqBaseStats.ItemsSource = baseStats;
            EqRandomStats.ItemsSource = randomStats;
            EqRandomAugmentationStats.ItemsSource = augmentationStats;
            EqTitanSkills.ItemsSource = titanSkills;

            switch (item.AugmentationLevel)
            {
                case 1:
                    EqAugmentedLevel.Content = "Augmented";
                    EqAugmentedLevel.Foreground = Brushes.Goldenrod;
                    break;
                case 2:
                    EqAugmentedLevel.Content = "Titaned";
                    EqAugmentedLevel.Foreground = Brushes.MediumVioletRed;
                    break;
                default:
                    EqAugmentedLevel.Content = "";
                    EqAugmentedLevel.Foreground = Brushes.White;
                    break;
            }
        }

        public void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            TofData.LoadEquipmentDataFromDb();
            RefreshEquipments();
        }


        public void Equipments_Click(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                var eq = (item.Content as EquipmentForTable)!;
                var equip = TofData.Equipments.First(e => e.Id == eq.Id);

                ShowEquipmentInfo(equip);
            }
        }

        public void SaveToJsonFile_Click(object sender, RoutedEventArgs e)
        {
            TofData.SaveEquipmentToFile();
        }
        
        public void LoadFromFileJson_Click(object sender, RoutedEventArgs e)
        {
            TofData.LoadEquipmentFromFileToMemory();
            TofData.SaveEquipmentFromMemoryToDb();
            RefreshEquipments();
        }

        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void AddItem_Click(object sender, RoutedEventArgs e)
        {
            var window = new CurrentEquipmentWindow(this);            
            window.Show();
        }

        public void DeleteEquipment_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                var equip = TofData.Equipments.First(e => e.Id == (int)(button.Tag));
                var messageBoxResult = MessageBox.Show($"Delete equip {equip.EquipmentType.Name} with id {equip.Id}?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    TofData.DeleteEquipment(equip);
                    RefreshEquipments();
                }
            }
        }
    }


    public class EquipmentForTable
    {
        public int Id { get; set; }
        public string EqName { get; set; } = null!;
        public string Stat1 { get; set; } = null!;
        public string Stat2 { get; set; } = null!;
        public string Stat3 { get; set; } = null!;
        public string Stat4 { get; set; } = null!;
        public int Stars { get; set; }

        public EquipmentForTable(Equipment eq)
        {
            Id = eq.Id;
            EqName = eq.EquipmentType.ShortName;
            Stat1 = $"{eq.Stats[0].CurrentStat.Name}: {eq.Stats[0].Value}";
            Stat2 = $"{eq.Stats[1].CurrentStat.Name}: {eq.Stats[1].Value}";
            Stat3 = $"{eq.Stats[2].CurrentStat.Name}: {eq.Stats[2].Value}";
            Stat4 = $"{eq.Stats[3].CurrentStat.Name}: {eq.Stats[3].Value}";
            Stars = eq.NumberOfStars;
        }
    }


}