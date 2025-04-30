using ZooFeedingCostCalculator.Models;

namespace ZooFeedingCostCalculator.Interfaces
{
    public interface IPriceProvider
    {
        FoodPrices GetPrices(string filePath);
    }
}
