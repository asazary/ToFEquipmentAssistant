using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using ToFEA.Model;

namespace ToFEA
{
    internal static class BestSetActions
    {
        private const int VariantsCount = 15;

        //private const int EquipmentTypesCount = 15;


        internal static List<Variant> CalculateVariants(int baseAttack, int baseRes, int mainStatId, int secondStatId)
        {

            var eqTypeIds = TofData.EquipmentTypes.OrderBy(e => e.Id)
                .Select(e => e.Id).ToList();
            var eqTypesCount = eqTypeIds.Count;
                        
            var allEquips = TofData.Equipments.GroupBy(e => e.EquipmentType.Id)
                .ToDictionary(g => g.Key, g => g.OrderBy(s => s.EquipmentType.Id).ToArray());

            var variants = new List<Variant>();

            foreach (var (typeId, equips) in allEquips.Take(eqTypesCount).ToList())
            {
                variants = GetBestVariants(variants.ToList(), equips.ToList(), baseAttack, baseRes, mainStatId, secondStatId);
            }
            return variants.Take(VariantsCount).ToList();
        }


        internal static List<Variant> GetBestVariants(List<Variant> vars, List<Equipment> nextColumn, int baseAttack, int baseRes,
            int mainStatId, int secondStatId)
        {
            var newArr = new List<Variant>(vars.Any() ? vars.Count * nextColumn.Count : nextColumn.Count);

            if (vars.Any())
                foreach (var curVar in vars)
                {
                    foreach (var nextEq in nextColumn)
                    {
                        var newLst = new List<Equipment>(curVar.Equips) { nextEq };
                        var newVar = new Variant(curVar, nextEq);

                        newArr.Add(newVar);
                    }
                }
            else
            {
                nextColumn.ForEach(eq => newArr.Add(new Variant(eq)));             
            }

            var tempArr = new List<Variant>(newArr.Count);
            foreach (var curVar in newArr)
                tempArr.Add(Variant.GetRecalculatedCopy(curVar, baseAttack, baseRes, true));

            var result = secondStatId > 0 ?
                     (from nw in newArr
                      join tp in tempArr on nw.Id equals tp.Id
                      orderby tp.StatValues[mainStatId] descending, tp.StatValues[secondStatId] descending
                      select nw)
                      .ToList() :
                      (from nw in newArr
                       join tp in tempArr on nw.Id equals tp.Id
                       orderby tp.StatValues[mainStatId] descending
                       select nw)
                      .ToList();

            return result;
        }        

    }


    public struct VariantStats
    {
        public double MainStat;
        public double SecondStat;
    }

    public class Variant
    {
        private static int _id = 0;

        public int Id { get; private set; }
        public List<Equipment> Equips { get; set; } = new();
        public Dictionary<int, double> StatValues { get; set; } = new();

        private Variant() { }

        public Variant(Variant source, bool needSameIds = false)
        {
            Id = needSameIds ? source.Id : ++_id;
            Equips = new List<Equipment>(source.Equips);
            StatValues = source.StatValues.ToDictionary(e => e.Key, e => e.Value);
        }

        public Variant(Equipment eq)
        {
            Id = ++_id;
            Equips = new List<Equipment> { eq };
            StatValues = TofData.PossibleStats.ToDictionary(s => s.Id, s => 0.0);
            eq.Stats.ForEach(s => StatValues[s.CurrentStat.Id] += s.Value);
        }

        public Variant(Variant source, Equipment eq)
        {
            Id = ++_id;
            Equips = new List<Equipment>(source.Equips) { eq };
            var newStatVals = source.StatValues.ToDictionary(e => e.Key, e => e.Value);
            eq.Stats.ForEach(s => newStatVals[s.CurrentStat.Id] += s.Value);
            StatValues = newStatVals;
        }

        public static Variant GetRecalculatedCopy(Variant source, double baseAttack, double baseRes, bool needSameIds = false)
        {
            var newVar = new Variant(source, needSameIds);

            foreach (var linkedStat in TofData.StatLinks.Where(s => s.CommonStat.Id == TofData.AtkStatId))
                newVar.StatValues[linkedStat.ElemStat.Id] += newVar.StatValues[TofData.AtkStatId]
                    + (linkedStat.PercentStat?.Id != null && linkedStat.PercentStat?.Id != 0
                    ? baseAttack * (newVar.StatValues[linkedStat.PercentStat!.Id] + 1.0) : 0.0);

            foreach (var linkedStat in TofData.StatLinks.Where(s => s.CommonStat.Id == TofData.ResStatId))
                newVar.StatValues[linkedStat.ElemStat.Id] += newVar.StatValues[TofData.ResStatId]
                    + (linkedStat.PercentStat?.Id != null && linkedStat.PercentStat?.Id != 0
                    ? baseRes * (newVar.StatValues[linkedStat.PercentStat!.Id] + 1.0) : 0.0);

            return newVar;
        }
    }

    public struct StatValue
    {
        public PossibleStat Stat;
        public double Value;
    }



}
