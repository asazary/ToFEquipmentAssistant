namespace ToFEA.JsonClasses
{
    internal class AllEquipmentJson
    {
        public List<EquipmentJson> Equipments { get; set; } = new();
    }

    internal class EquipmentJson
    {
        public int Id { get; set; }
        public int EquipmentTypeId { get; set; }
        public int NumberOfStars { get; set; } = 0;
        public int AugmentationLevel { get; set; } = 0; // 0, 1, 2
        public List<StatJson> Stats { get; set; } = new();
        public List<int> BaseStatsIds { get; set; } = new();
        public List<AugmentationStatJson> AugmentationStats { get; set; } = new();
        public List<TitanStatsJson> TitanStats { get; set; } = new();
    }
}
