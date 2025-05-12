using System;
using Microsoft.Extensions.DependencyInjection;
using ZooFeedingCostCalculator.Interfaces;
using ZooFeedingCostCalculator.Models;
using ZooFeedingCostCalculator.Services;

namespace ZooFeedingCostCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Setup DI
                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);
                var serviceProvider = serviceCollection.BuildServiceProvider();

                // Get services
                var priceProvider = serviceProvider.GetRequiredService<IPriceProvider>();
                var speciesProvider = serviceProvider.GetRequiredService<IAnimalSpeciesProvider>();
                var zooProvider = serviceProvider.GetRequiredService<IZooProvider>();
                var feedingCalculator = serviceProvider.GetRequiredService<IFeedingCostCalculator>();

                // File paths
                var pricesFile = "prices.txt";
                var speciesFile = "animals.csv";
                var zooFile = "zoo.xml";

                // Load data
                FoodPrices prices = priceProvider.GetPrices(pricesFile);
                var species = speciesProvider.GetAnimalSpecies(speciesFile);
                var animals = zooProvider.GetAnimals(zooFile);

                // Calculate total cost
                decimal totalCost = feedingCalculator.CalculateTotalCost(animals, species, prices);

                Console.WriteLine($"Total daily feeding cost: {totalCost:C}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IPriceProvider, PriceProvider>();
            services.AddSingleton<IAnimalSpeciesProvider, AnimalSpeciesProvider>();
            services.AddSingleton<IZooProvider, ZooProvider>();
            services.AddSingleton<IFeedingCostCalculator, FeedingCostCalculator>();
        }
    }
}
