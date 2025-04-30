using System.IO;
using System.Threading.Tasks;
using Xunit;
using ZooFeedingCostCalculator.Services;

namespace ZooFeedingCostCalculator.Tests
{
    public class PriceProviderTests
    {
        [Fact]
        public void GetPrices_ParsesPricesCorrectly()
        {
            // Arrange
            var content = "Meat=12.56\nFruit=5.60";
            var tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, content);

            var provider = new PriceProvider();

            // Act
            var prices = provider.GetPrices(tempFile);

            // Assert
            Assert.Equal(12.56m, prices.MeatPricePerKg);
            Assert.Equal(5.60m, prices.FruitPricePerKg);

            File.Delete(tempFile);
        }
    }
}
