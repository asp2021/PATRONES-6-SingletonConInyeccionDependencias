using NUnit.Framework;
using SingletonConInyeccionDependencias;
using System.Collections.Generic;

namespace UnitTest1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var finder = new SingletonFinder(new DummyDatabase());
            var total = finder.GetPopulation(new[] { "Santiago", "Buenos Aires" });
            Assert.AreEqual(7000 + 2000, total);
        }

        public class DummyDatabase : ISingletonContainer
        {
            public int GetPopulation(string name)
            {
                return new Dictionary<string, int>
                {
                    ["Santiago"] = 7000,
                    ["Buenos Aires"] = 2000
                }[name];
            }
        }
    }
}