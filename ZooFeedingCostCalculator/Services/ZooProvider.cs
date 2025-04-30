using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using ZooFeedingCostCalculator.Interfaces;
using ZooFeedingCostCalculator.Models;

namespace ZooFeedingCostCalculator.Services
{
    public class ZooProvider : IZooProvider
    {
        public IEnumerable<AnimalInstance> GetAnimals(string filePath)
        {
            var doc = XDocument.Load(filePath);
            var animals = new List<AnimalInstance>();

            var zooElement = doc.Element("Zoo");
            if (zooElement == null)
                return animals;

            foreach (var speciesGroup in zooElement.Elements())
            {
                foreach (var animalElement in speciesGroup.Elements())
                {
                    var speciesName = animalElement.Name.LocalName;
                    var nameAttr = animalElement.Attribute("name");
                    var kgAttr = animalElement.Attribute("kg");

                    if (nameAttr == null || kgAttr == null)
                        continue;

                    if (!decimal.TryParse(kgAttr.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var weightKg))
                        continue;

                    animals.Add(new AnimalInstance
                    {
                        SpeciesName = speciesName,
                        Name = nameAttr.Value,
                        WeightKg = weightKg
                    });
                }
            }

            return animals;
        }
    }
}
