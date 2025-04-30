using System;
using System.IO;
using ZooFeedingCostCalculator.Interfaces;
using ZooFeedingCostCalculator.Models;

namespace ZooFeedingCostCalculator.Services
{
    public class PriceProvider : IPriceProvider
    {
        public FoodPrices GetPrices(string filePath)
        {
            var prices = new FoodPrices();

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('=');
                if (parts.Length != 2)
                    continue;

                var key = parts[0].Trim();
                var value = parts[1].Trim();

                if (key.Equals("Meat", StringComparison.OrdinalIgnoreCase))
                {
                    prices.MeatPricePerKg = decimal.Parse(value);
                }
                else if (key.Equals("Fruit", StringComparison.OrdinalIgnoreCase))
                {
                    prices.FruitPricePerKg = decimal.Parse(value);
                }
            }

            return prices;
        }
    }
}
