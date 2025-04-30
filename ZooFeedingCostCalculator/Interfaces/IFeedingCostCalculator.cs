using System.Collections.Generic;
using ZooFeedingCostCalculator.Models;

namespace ZooFeedingCostCalculator.Interfaces
{
    public interface IFeedingCostCalculator
    {
        decimal CalculateTotalCost(IEnumerable<AnimalInstance> animals, IEnumerable<AnimalSpecies> species, FoodPrices prices);
    }
}
