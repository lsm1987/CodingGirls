using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// 시나리오 리스트의 한 항목
/// </summary>
public class UIScenarioItem : UIWindow
{
    [SerializeField]
    private Button _btn;
    [SerializeField]
    private Text _number;
    [SerializeField]
    private Text _title;
    [SerializeField]
    private Text _tag;

    public void Set(ScenarioList.Item item, UnityAction onClick)
    {
        _number.text = item._ID.ToString();
        _title.text = item._title;

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < item._tags.Count; ++i)
        {
            if (i != 0)
            {
                sb.Append(", ");
            }
            sb.Append(item._tags[i]);
        }
        _tag.text = sb.ToString();

        _btn.onClick.AddListener(onClick);
    }
}