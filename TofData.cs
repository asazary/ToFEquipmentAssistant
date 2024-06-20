using TofEA.Model;
using System.Text.Json;
using System.IO;
using ToFEA.JsonClasses;
using ToFEA.Model;
using Microsoft.EntityFrameworkCore;

namespace ToFEA
{
    public static class TofData
    {
        public static List<EquipmentType> EquipmentTypes { get; set; } = new();
        public static List<PossibleStat> PossibleStats { get; set; } = new();
        public static List<PossibleTitanStat> PossibleTitanStats { get; set; } = new();
        public static List<BaseStat> BaseStats { get; set; } = new();
        public static List<Equipment> Equipments { get; set; } = new();
        public static List<TitanStat> TitanStats { get; set; } = new();
        public static List<Stat> Stats { get; set; } = new();
        public static List<AugmentationStat> AugmentationStats { get; set; } = new();

        public static List<StatLink> StatLinks { get; set; } = new();

        public static int AtkStatId { get; set; }
        public static int ResStatId {  get; set; }

        const string BaseDataFilePath = "basedata.json";
        const string DbPath = "tofeadata.db";
        const string EquipmentFile = "equipment.json";
        public const string ImagesPath = "images";


        public static void InitDb()
        {
            if (File.Exists(DbPath)) return;
            
            var options = new JsonSerializerOptions
            {
                ReadCommentHandling = JsonCommentHandling.Skip
            };

            using (var fs = new FileStream(BaseDataFilePath, FileMode.Open))
            {
                var jsonFile = JsonSerializer.Deserialize<JsonFile>(fs, options);

                using (var db = new ApplicationContext())
                {
                    if (db.EquipmentTypes.Any()) return;

                    var possibleTitanStats = jsonFile!.PossibleTitanStats
                        .OrderBy(e => e.Id)
                        .Select(e => new PossibleTitanStat
                        {
                            Id = e.Id,
                            Name = e.Name
                        })
                        .ToList();
                    db.AddRange(possibleTitanStats);

                    var possibleStats = jsonFile!.PossibleStats
                        .OrderBy(e => e.Id)
                        .Select(e => new PossibleStat
                        {
                            Id = e.Id,
                            Name = e.Name,
                            IsPercent = e.IsPercent,
                            InitValue = e.InitValue,
                            MinUpgradeValue = e.MinUpgradeValue,
                            MaxUpgradeValue = e.MaxUpgradeValue
                        })
                        .ToList();
                    db.AddRange(possibleStats);

                    var equipmentTypes = jsonFile!.EquipmentType
                        .OrderBy(e => e.Id)
                        .Select(e => new EquipmentType
                        {
                            Id = e.Id,
                            Name = e.Name,
                            ShortName = e.ShortName,
                            Generation = e.Generation,
                            IconName = e.IconName,
                            PossibleStats = possibleStats.Where(stat => e.PossibleStats.Contains(stat.Id)).OrderBy(eq => eq.Id).ToList(),
                            PossibleTitanStats = possibleTitanStats.Where(stat => e.PossibleTitanStats.Contains(stat.Id)).OrderBy(eq => eq.Id).ToList()
                        })
                        .ToList();
                    db.AddRange(equipmentTypes);

                    foreach (var stat in jsonFile!.BaseStats.OrderBy(e => e.Id))
                    {
                        var newStat = new BaseStat
                        {
                            Id = stat.Id,
                            Slot = equipmentTypes.First(e => e.Id == stat.SlotId),
                            Stat = possibleStats.First(e => e.Id == stat.StatId),
                            Value1 = stat.Value1,
                            Value2 = stat.Value2,
                            Value3 = stat.Value3
                        };
                        db.Add(newStat);
                    }

                    var statLinks = jsonFile!.StatLinks
                        .Select(s => new StatLink
                        {
                            Id = s.Id,
                            ElemStat = possibleStats.First(e => e.Id == s.ElemStat),
                            CommonStat = possibleStats.First(e => e.Id == s.CommonStat),
                            PercentStat = possibleStats.FirstOrDefault(e => e.Id == s.PercentStat),
                            DamageStat = possibleStats.FirstOrDefault(e => e.Id == s.DamageStat)
                        })
                        .ToList();
                    db.AddRange(statLinks);

                    db.SaveChanges();
                }
            }
        }

        public static void InitDbTestData()
        {
            if (!File.Exists(DbPath)) return;
            
            using (var db = new ApplicationContext())
            {
                var eq1 = new Equipment()
                {
                    Id = 1,
                    EquipmentType = db.EquipmentTypes.First(e => e.Id == 1),
                    NumberOfStars = 5,
                    BaseStats =
                        [
                            db.BaseStats.First(e => e.Id == 1),
                            db.BaseStats.First(e => e.Id == 2)
                        ],
                    TitanStats = new(),
                    Stats =
                        [
                            new Stat
                            {
                                Id = 1,
                                CurrentStat = db.PossibleStats.First(e => e.Id == 1),
                                Value = 120
                            },
                            new Stat
                            {
                                Id = 2,
                                CurrentStat = db.PossibleStats.First(e => e.Id == 2),
                                Value = 420
                            },
                            new Stat
                            {
                                Id = 3,
                                CurrentStat = db.PossibleStats.First(e => e.Id == 3),
                                Value = 300
                            },
                            new Stat
                            {
                                Id = 4,
                                CurrentStat = db.PossibleStats.First(e => e.Id == 14),
                                Value = 5200
                            }
                        ]
                };
                var eq2 = new Equipment()
                {
                    Id = 2,
                    EquipmentType = db.EquipmentTypes.First(e => e.Id == 2),
                    NumberOfStars = 5,
                    AugmentationLevel = 2,
                    BaseStats = new List<BaseStat>
                        {
                            db.BaseStats.First(e => e.Id == 5),
                            db.BaseStats.First(e => e.Id == 6)
                        },
                    TitanStats = new(),
                    Stats =
                        [
                            new Stat
                            {
                                Id = 5,
                                CurrentStat = db.PossibleStats.First(e => e.Id == 1),
                                Value = 120
                            },
                            new Stat
                            {
                                Id = 6,
                                CurrentStat = db.PossibleStats.First(e => e.Id == 16),
                                Value = 420
                            },
                            new Stat
                            {
                                Id = 7,
                                CurrentStat = db.PossibleStats.First(e => e.Id == 18),
                                Value = 1300
                            },
                            new Stat
                            {
                                Id = 8,
                                CurrentStat = db.PossibleStats.First(e => e.Id == 4),
                                Value = 700
                            }
                        ],
                    AugmentationStats =
                        [
                            new AugmentationStat
                            {
                                Id = 1,
                                CurrentStat = db.PossibleStats.First(e => e.Id == 19),
                                Value = 1400
                            },
                            new AugmentationStat
                            {
                                Id = 2,
                                CurrentStat = db.PossibleStats.First(e => e.Id == 20),
                                Value = 1450
                            }
                        ]
                };

                db.AddRange(eq1, eq2);
                db.SaveChanges();
            }
        }

        public static void LoadDataFromDb()
        {
            using (var db = new ApplicationContext())
            {
                EquipmentTypes = db.EquipmentTypes
                    .Include(u => u.PossibleTitanStats)
                    .Include(u => u.PossibleStats)
                    .Include(u => u.BaseStats)
                    .ToList();

                PossibleStats = db.PossibleStats.Include(u => u.EquipmentType).ToList();
                PossibleTitanStats = db.PossibleTitanStats.Include(u => u.EquipmentTypes).ToList();
                BaseStats = db.BaseStats.Include(u => u.Slot).Include(u => u.Stat).ToList();
                StatLinks = db.StatLinks.Include(u => u.ElemStat).ToList();

                Equipments = db.Equipments
                    .Include(u => u.EquipmentType)
                    .Include(u => u.Stats)
                        .ThenInclude(s => s.CurrentStat)
                    .Include (u => u.BaseStats)
                        .ThenInclude(s => s.Stat)
                    .Include(u => u.TitanStats)
                        .ThenInclude(s => s.CurrentTitanStat)
                    .Include(u => u.AugmentationStats)
                        .ThenInclude(s => s.CurrentStat)
                    .ToList();

                AtkStatId = db.PossibleStats.First(s => s.Name.Equals("ATK")).Id;
                ResStatId = db.PossibleStats.First(s => s.Name.Equals("RES")).Id;
            }
        }

        public static void LoadEquipmentDataFromDb(int id = 0)
        {
            using (var db = new ApplicationContext())
            {
                IQueryable<Equipment> query = db.Equipments;

                if (id > 0)
                {
                    Equipments.AddRange(query.Where(u => u.Id == id)
                        .Include(u => u.EquipmentType)
                        .Include(u => u.Stats)
                            .ThenInclude(s => s.CurrentStat)
                        .Include(u => u.BaseStats)
                            .ThenInclude(s => s.Stat)
                        .Include(u => u.TitanStats)
                            .ThenInclude(s => s.CurrentTitanStat)
                        .Include(u => u.AugmentationStats)
                            .ThenInclude(s => s.CurrentStat)
                        .ToList());
                }
                else
                {
                    Equipments = query
                        .Include(u => u.EquipmentType)
                        .Include(u => u.Stats)
                            .ThenInclude(s => s.CurrentStat)
                        .Include(u => u.BaseStats)
                            .ThenInclude(s => s.Stat)
                        .Include(u => u.TitanStats)
                            .ThenInclude(s => s.CurrentTitanStat)
                        .Include(u => u.AugmentationStats)
                            .ThenInclude(s => s.CurrentStat)
                        .ToList();
                }                
            }
        }

        internal static Equipment GetEquipmentToAdd(Equipment eq, ApplicationContext db)
        {     
            var result = new Equipment()
            {
                Id = eq.Id,
                EquipmentType = db.EquipmentTypes.First(e => e.Id == eq.EquipmentType.Id),
                NumberOfStars = eq.NumberOfStars,
                AugmentationLevel = eq.AugmentationLevel,
                Stats = eq.Stats.Select(s => new Stat
                {
                    Id = s.Id,
                    CurrentStat = db.PossibleStats.First(st => st.Id == s.CurrentStat.Id),
                    Value = s.Value
                }).ToList(),
                BaseStats = db.BaseStats.Where(s => eq.BaseStats.Select(st => st.Id).Contains(s.Id)).ToList(),
                AugmentationStats = eq.AugmentationLevel >= 1 ? eq.AugmentationStats.Select(s => new AugmentationStat
                {
                    Id = s.Id,
                    CurrentStat = db.PossibleStats.First(st => st.Id == s.CurrentStat.Id),
                    Value = s.Value
                }).ToList() : [],
                TitanStats = eq.AugmentationLevel >= 2 ? eq.TitanStats.Select(s => new TitanStat
                {
                    Id = s.Id,
                    CurrentTitanStat = db.PossibleTitanStats.First(st => st.Id == s.CurrentTitanStat.Id),
                    StatLevel = s.StatLevel
                }).ToList() : []
            };
            return result;
        }

        internal static void DeleteEquipment(Equipment equip)
        {
            using (var db = new ApplicationContext())
            {
                db.Remove(db.Equipments.First(e => e.Id == equip.Id));
                db.SaveChanges();
                Equipments.Remove(equip);
            }
        }

        internal static void AddEquipment(Equipment equip)
        {
            var newEquipId = 0;

            using (var db = new ApplicationContext())
            {
                var newEquip = new Equipment()
                {
                    Id = equip.Id,
                    EquipmentType = db.EquipmentTypes.First(e => e.Id == equip.EquipmentType.Id),
                    NumberOfStars = equip.NumberOfStars,
                    AugmentationLevel = equip.AugmentationLevel,
                    Stats = equip.Stats.Select(s => new Stat
                    {
                        Id = s.Id,
                        CurrentStat = db.PossibleStats.First(st => st.Id == s.CurrentStat.Id),
                        Value = s.Value
                    }).ToList(),
                    BaseStats = db.BaseStats.Where(s => equip.BaseStats.Select(st => st.Id).Contains(s.Id)).ToList(),
                    AugmentationStats = equip.AugmentationLevel >= 1 ? equip.AugmentationStats.Select(s => new AugmentationStat
                    {
                        Id = s.Id,
                        CurrentStat = db.PossibleStats.First(st => st.Id == s.CurrentStat.Id),
                        Value = s.Value
                    }).ToList() : [],
                    TitanStats = equip.AugmentationLevel >= 2 ? equip.TitanStats.Select(s => new TitanStat
                    {
                        Id = s.Id,
                        CurrentTitanStat = db.PossibleTitanStats.First(st => st.Id == s.CurrentTitanStat.Id),
                        StatLevel = s.StatLevel
                    }).ToList() : []
                };

                db.Equipments.Add(newEquip);
                db.SaveChanges();
                newEquipId = newEquip.Id;
            }

            if (newEquipId != 0) 
                LoadEquipmentDataFromDb(newEquipId);
        }


        internal static void SaveEquipmentToFile()
        {
            var equipments = Equipments
                .Select(e => new EquipmentJson
                {
                    Id = e.Id,
                    EquipmentTypeId = e.EquipmentType.Id,
                    NumberOfStars = e.NumberOfStars,
                    AugmentationLevel = e.AugmentationLevel,
                    Stats = e.Stats.Select(s => new StatJson
                    {
                        Id = s.Id,
                        StatId = s.CurrentStat.Id,
                        Value = s.Value
                    }).ToList(),
                    BaseStatsIds = e.BaseStats.Select(s => s.Id).ToList(),
                    AugmentationStats = e.AugmentationStats.Select(s => new AugmentationStatJson
                    {
                        Id = s.Id,
                        StatId = s.CurrentStat.Id,
                        Value = s.Value
                    }).ToList(),
                    TitanStats = e.TitanStats.Select(s => new TitanStatsJson
                    {
                        Id = s.Id,
                        TitanStatId = s.CurrentTitanStat.Id,
                        StatLevel = s.StatLevel
                    }).ToList()
                }).ToList();

            var json = JsonSerializer.Serialize<AllEquipmentJson>(new AllEquipmentJson { Equipments = equipments });

            File.WriteAllText(EquipmentFile, json);
        }

        internal static void LoadEquipmentFromFileToMemory()
        {
            using (var fs = new FileStream(EquipmentFile, FileMode.Open))
            {
                var equipmentFromFile = JsonSerializer.Deserialize<AllEquipmentJson>(fs);

                Stats.Clear();
                AugmentationStats.Clear();
                TitanStats.Clear();
                Equipments.Clear();

                foreach (var equip in equipmentFromFile!.Equipments)
                {
                    foreach (var stat in equip.Stats)
                    {
                        Stats.Add(new Stat
                        {
                            Id = stat.Id,
                            CurrentStat = PossibleStats.First(e => e.Id == stat.StatId),
                            Value = stat.Value
                        });
                    }

                    foreach (var augStat in equip.AugmentationStats)
                    {
                        AugmentationStats.Add(new AugmentationStat
                        {
                            Id = augStat.Id,
                            CurrentStat = PossibleStats.First(e => e.Id == augStat.StatId),
                            Value = augStat.Value
                        });
                    }

                    foreach (var titanStat in equip.TitanStats)
                    {
                        TitanStats.Add(new TitanStat
                        {
                            Id = titanStat.Id,
                            CurrentTitanStat = PossibleTitanStats.First(s => s.Id == titanStat.TitanStatId),
                            StatLevel = titanStat.StatLevel
                        });
                    }

                    //var t1 = EquipmentTypes.First(s => s.Id == equip.Id);
                    var t2 = equip.NumberOfStars;
                    var t3 = equip.AugmentationLevel;
                    var t4 = Stats.Where(s => equip.Stats.Select(st => st.Id).Contains(s.Id)).ToList();
                    var t5 = BaseStats.Where(s => equip.BaseStatsIds.Contains(s.Id)).ToList();
                    var t6 = AugmentationStats.Where(s => equip.AugmentationStats.Select(st => st.Id).Contains(s.Id)).ToList();
                    var t7 = TitanStats.Where(s => equip.TitanStats.Select(st => st.Id).Contains(s.Id)).ToList();

                    Equipments.Add(new Equipment
                    {
                        Id = equip.Id,
                        EquipmentType = EquipmentTypes.First(s => s.Id == equip.EquipmentTypeId),
                        NumberOfStars = equip.NumberOfStars,
                        AugmentationLevel = equip.AugmentationLevel,
                        Stats = Stats.Where(s => equip.Stats.Select(st => st.Id).Contains(s.Id)).ToList(),
                        BaseStats = BaseStats.Where(s => equip.BaseStatsIds.Contains(s.Id)).ToList(),
                        AugmentationStats = AugmentationStats.Where(s => equip.AugmentationStats.Select(st => st.Id).Contains(s.Id)).ToList(),
                        TitanStats = TitanStats.Where(s => equip.TitanStats.Select(st => st.Id).Contains(s.Id)).ToList()
                    });
                }
            }
        }

        internal static void SaveEquipmentFromMemoryToDb()
        {
            using (var db = new ApplicationContext())
            {
                db.AddRange(Equipments.Select(e => GetEquipmentToAdd(e, db)).ToList());

                db.SaveChanges();
            }
        }
    }    
}
