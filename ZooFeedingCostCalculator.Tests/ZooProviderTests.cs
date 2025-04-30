using System.IO;
using System.Linq;
using System.Xml.Linq;
using Xunit;
using ZooFeedingCostCalculator.Services;

namespace ZooFeedingCostCalculator.Tests
{
    public class ZooProviderTests
    {
        [Fact]
        public void GetAnimals_ParsesAnimalsCorrectly()
        {
            // Arrange
            var xmlContent = @"
<Zoo>
    <Lions>
        <Lion name='Simba' kg='160'/>
        <Lion name='Nala' kg='172'/>
    </Lions>
    <Giraffes>
        <Giraffe name='Hanna' kg='200'/>
    </Giraffes>
</Zoo>";
            var tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, xmlContent);

            var provider = new ZooProvider();

            // Act
            var animals = provider.GetAnimals(tempFile).ToList();

            // Assert
            Assert.Equal(3, animals.Count);

            var simba = animals.FirstOrDefault(a => a.Name == "Simba");
            Assert.NotNull(simba);
            Assert.Equal("Lion", simba.SpeciesName);
            Assert.Equal(160m, simba.WeightKg);

            var hanna = animals.FirstOrDefault(a => a.Name == "Hanna");
            Assert.NotNull(hanna);
            Assert.Equal("Giraffe", hanna.SpeciesName);
            Assert.Equal(200m, hanna.WeightKg);

            File.Delete(tempFile);
        }
    }
}
