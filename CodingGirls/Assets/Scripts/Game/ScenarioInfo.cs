using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 시나리오 관련 설정
    /// </summary>
    public class ScenarioInfo
    {
        public string _defaultMainFileName;
        public Dictionary<string, Color32> _nameColors = new Dictionary<string, Color32>();
        public Color32 _defaultNameColor = new Color32(255, 255, 255, 255);

        public Color GetNameColor(string name)
        {
            Color32 color;
            if (_nameColors.TryGetValue(name, out color))
            {
                return color;
            }
            else
            {
                return _defaultNameColor;
            }
        }

        public static ScenarioInfo ConvertFromJson(ScenarioInfoJson json)
        {
            ScenarioInfo info = new ScenarioInfo();
            info._defaultMainFileName = json._defaultMainFileName;
            foreach (ScenarioInfoJson.NameColor nameColor in json._nameColors)
            {
                info._nameColors.Add(nameColor._name, nameColor._color);
            }
            info._defaultNameColor = json._defaultNameColor;
            return info;
        }

        public static ScenarioInfoJson ConvertToJson(ScenarioInfo info)
        {
            ScenarioInfoJson json = new ScenarioInfoJson();
            json._defaultMainFileName = info._defaultMainFileName;
            foreach (var pair in info._nameColors)
            {
                json._nameColors.Add(new ScenarioInfoJson.NameColor(pair.Key, pair.Value));
            }
            json._defaultNameColor = info._defaultNameColor;
            return json;
        }

        public string ToAssertableString()
        {
            string str = "";
            str += _defaultMainFileName;
            str += ";";
            foreach (var pair in _nameColors)
            {
                str += ("(" + pair.Key + "," + pair.Value + ")");
            }
            str += ";";
            str += _defaultNameColor.ToString();
            return str;
        }
    }

    /// <summary>
    /// 시나리오 관련 설정을 JSON 으로 읽고 저장하기 위한 중간 자료구조
    /// </summary>
    [Serializable]
    public class ScenarioInfoJson
    {
        public string _defaultMainFileName;
        public List<NameColor> _nameColors = new List<NameColor>();
        public Color32 _defaultNameColor = new Color32(255, 255, 255, 255);

        [Serializable]
        public class NameColor
        {
            public string _name;
            public Color32 _color;

            public NameColor(string name, Color32 color)
            {
                _name = name;
                _color = color;
            }
        }
    }
}