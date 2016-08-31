using NUnit.Framework;

namespace Game
{
    public class ScenarioManagerTest
    {
        [Test]
        public void InitializeTest()
        {
            ScenarioManager _scenarioManager = new ScenarioManager();
            Assert.DoesNotThrow(() => _scenarioManager.Initialize());
        }
    }
}