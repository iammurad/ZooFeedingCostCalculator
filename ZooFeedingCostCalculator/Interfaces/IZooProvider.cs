using System.Collections.Generic;
using ZooFeedingCostCalculator.Models;

namespace ZooFeedingCostCalculator.Interfaces
{
    public interface IZooProvider
    {
        IEnumerable<AnimalInstance> GetAnimals(string filePath);
    }
}
