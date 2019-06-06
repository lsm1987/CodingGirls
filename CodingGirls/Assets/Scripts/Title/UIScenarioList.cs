using UnityEngine;
using UnityEngine.UI;

public class UIScenarioList : UIWindow
{
    [SerializeField]
    private RectTransform _contents = null;
    [SerializeField]
    private GameObject _itemPrefab = null;
    [SerializeField]
    private Button _btnClose = null;

    private void Awake()
    {
        InitializeItems();
        _btnClose.onClick.AddListener(OnClickBack);
    }

    public override bool OnKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClickBack();
        }

        return true;
    }

    private void InitializeItems()
    {
        TextAsset loadedText = Resources.Load<TextAsset>(Define._scenarioListPath);
        ScenarioList scenarioList = JsonUtility.FromJson<ScenarioList>(loadedText.text);

        for (int i = 0; i < scenarioList._items.Count; ++i)
        {
            AddItem(scenarioList._items[i]);
        }
    }

    private void AddItem(ScenarioList.Item item)
    {
        UIScenarioItem itemUI = UIManager.CreateWidget<UIScenarioItem>(_itemPrefab, _contents);
        itemUI.Set(item
            , delegate { OnItemSelected(item._ID); }
            );
    }

    private void OnItemSelected(int id)
    {
        PlaySelectSound();
        App.AppSystem._GameSystemParam.Set(GetScenarioName(id));
        TitleSystem._Instance.DoGameStartSequence();
    }

    private void OnClickBack()
    {
        PlaySelectSound();
        TitleSystem._Instance._UIManager.CloseScenarioList();
        TitleSystem._Instance._UIManager.OpenTitleMenu();
    }

    private string GetScenarioName(int id)
    {
        return string.Format("Scenario{0:D3}", id);
    }

    private void PlaySelectSound()
    {
        SoundManager._Instance.PlaySound(Define._menuSelectSound);
    }
}