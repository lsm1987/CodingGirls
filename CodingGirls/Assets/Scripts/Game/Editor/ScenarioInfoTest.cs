using UnityEngine;
using NUnit.Framework;

namespace Game
{
    public static class ScenarioInfoTest
    {
        [Test]
        public static void ScenarioInfoConvertTest()
        {
            ScenarioInfo info = new ScenarioInfo();
            info._defaultMainFileName = "Main";
            info._nameColors.Add("나래", new Color(0.5f, 0.5f, 0.5f));
            info._nameColors.Add("나", Color.blue);
            info._defaultNameColor = Color.red;

            ScenarioInfoJson jsonData = ScenarioInfo.ConvertToJson(info);
            string jsonString = JsonUtility.ToJson(jsonData, true);

            ScenarioInfoJson loadedJsonData = JsonUtility.FromJson<ScenarioInfoJson>(jsonString);
            ScenarioInfo loadedInfo = ScenarioInfo.ConvertFromJson(loadedJsonData);

            Assert.AreEqual(info.ToAssertableString(), loadedInfo.ToAssertableString());
        }
    }
}