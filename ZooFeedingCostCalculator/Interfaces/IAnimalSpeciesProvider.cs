using System.Collections.Generic;
using ZooFeedingCostCalculator.Models;

namespace ZooFeedingCostCalculator.Interfaces
{
    public interface IAnimalSpeciesProvider
    {
        IEnumerable<AnimalSpecies> GetAnimalSpecies(string filePath);
    }
}
