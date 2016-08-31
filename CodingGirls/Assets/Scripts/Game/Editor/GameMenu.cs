using UnityEditor;
using UnityEngine;

namespace Game
{
    public static class GameMenu
    {
        [MenuItem("Game/Create ScenarioInfo")]
        private static void CreatScenarioInfo()
        {
            ScenarioInfo info = new ScenarioInfo();
            info._defaultMainFileName = "Main";
            info._nameColors.Add("나래", new Color32(255, 167, 152, 255));
            info._defaultNameColor = Color.white;

            ScenarioInfoJson jsonData = ScenarioInfo.ConvertToJson(info);
            string jsonString = JsonUtility.ToJson(jsonData, true);
            System.IO.File.WriteAllText(Application.dataPath + "/Resources/" + Define._scenarioInfoPath + ".json", jsonString);
            AssetDatabase.Refresh();
        }

        [MenuItem("Game/Create ScenarioList")]
        private static void CreatScenarioList()
        {
            ScenarioList data = new ScenarioList();

            var item1 = new ScenarioList.Item();
            item1._ID = 1;
            item1._title = "TEST1";
            data._items.Add(item1);

            var item2 = new ScenarioList.Item();
            item2._ID = 2;
            item2._title = "TEST2";
            item2._tags.Add("Tag1");
            item2._tags.Add("Tag2");
            data._items.Add(item2);

            string jsonString = JsonUtility.ToJson(data, true);
            System.IO.File.WriteAllText(Application.dataPath + "/Resources/" + Define._scenarioListPath + ".json", jsonString);
            AssetDatabase.Refresh();
        }
    }
}