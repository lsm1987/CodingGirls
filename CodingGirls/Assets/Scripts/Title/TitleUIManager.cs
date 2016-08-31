using UnityEngine;
using UnityEngine.UI;

public class TitleUIManager : UIManager
{
    [SerializeField]
    private Image _portrait;
    [SerializeField]
    private Transform _mainPanel;
    public FadeOverlay _FadeOverlay { get; private set; }
    private UITitleMenu _titleMenu = null;
    public UITitleMenu _TitleMenu { get { return _titleMenu; } }
    private UIScenarioList _scenarioList = null;
    public UIScenarioList _ScenarioList { get { return _scenarioList; } }
    private UIAbout _about = null;

    public void Initailize()
    {
        SetPortrait();
        _FadeOverlay = GetComponentInChildren<FadeOverlay>(true);
        _FadeOverlay.DoFadeOut(0.0f);
        OpenTitleMenu();
    }

    public void OpenTitleMenu()
    {
        if (_titleMenu == null)
        {
            _titleMenu = OpenWindow(Define._uiTitleMenuPrefabPath, _mainPanel) as UITitleMenu;
        }
    }

    public void CloseTitleMenu()
    {
        if (_titleMenu != null)
        {
            CloseWindow(_titleMenu);
            _titleMenu = null;
        }
    }

    public void OpenScenarioList()
    {
        if (_scenarioList == null)
        {
            _scenarioList = OpenWindow(Define._uiScenarioListPrefabPath, _mainPanel) as UIScenarioList;
        }
    }

    public void CloseScenarioList()
    {
        if (_scenarioList != null)
        {
            CloseWindow(_scenarioList);
            _scenarioList = null;
        }
    }

    public void OpenAbout()
    {
        if (_about == null)
        {
            _about = OpenWindow(Define._uiAboutPrefabPath, _mainPanel) as UIAbout;
        }
    }

    public void CloseAbout()
    {
        if (_about != null)
        {
            CloseWindow(_about);
            _about = null;
        }
    }

    private void SetPortrait()
    {
        if (_portrait == null)
        {
            return;
        }

        _portrait.sprite = GetRandomPortraitSprite();
    }

    private Sprite GetRandomPortraitSprite()
    {
        int index = Random.Range(0, 2);
        string name;
        switch (index)
        {
            case 0:
                {
                    name = "Portrait_Epsilon";
                    break;
                }
            default:
                {
                    name = "Portrait_Haru";
                    break;
                }
        }

        return Resources.Load<Sprite>(string.Concat("Texture/", name));
    }
}