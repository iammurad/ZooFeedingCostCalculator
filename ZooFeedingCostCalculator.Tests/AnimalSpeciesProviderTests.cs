
using ZooFeedingCostCalculator.Services;

namespace ZooFeedingCostCalculator.Tests
{
    public class AnimalSpeciesProviderTests
    {
        [Fact]
        public void GetAnimalSpecies_ParsesSpeciesCorrectly()
        {
            // Arrange
            var content = "Lion;0.10;meat;\nTiger;0.09;meat;\nGiraffe;0.08;fruit;\nWolf;0.07;both;90%";
            var tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, content);

            var provider = new AnimalSpeciesProvider();

            // Act
            var species = provider.GetAnimalSpecies(tempFile).ToList();

            // Assert
            Assert.Equal(4, species.Count);

            var lion = species.FirstOrDefault(s => s.Name == "Lion");
            Assert.NotNull(lion);
            Assert.Equal(0.10m, lion.Rate);
            Assert.Equal(ZooFeedingCostCalculator.Models.FoodType.Meat, lion.FoodType);

            var wolf = species.FirstOrDefault(s => s.Name == "Wolf");
            Assert.NotNull(wolf);
            Assert.Equal(0.07m, wolf.Rate);
            Assert.Equal(ZooFeedingCostCalculator.Models.FoodType.Both, wolf.FoodType);
            Assert.Equal(90, wolf.MeatPercentage);

            File.Delete(tempFile);
        }
    }
}
