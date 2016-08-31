using System;
using System.Collections.Generic;

/// <summary>
/// 시나리오 정보 목록
/// </summary>
[Serializable]
public class ScenarioList
{
    /// <summary>
    /// 단일 시나리오 정보
    /// </summary>
    [Serializable]
    public class Item
    {
        public int _ID;
        public string _title;
        public List<string> _tags = new List<string>();
    }

    public List<Item> _items = new List<Item>();
}