using UnityEngine;
using UnityEngine.UI;

public class UITitleMenu : UIWindow
{
    [SerializeField]
    private Button _btnStart;
    [SerializeField]
    private Button _btnAbout;
    [SerializeField]
    private Button _btnExit;

    private void Awake()
    {
        _btnStart.onClick.AddListener(OnClickStart);
        _btnAbout.onClick.AddListener(OnClickAbout);
        _btnExit.onClick.AddListener(OnClickExit);
    }

    private void OnClickStart()
    {
        PlaySelectSound();
        TitleSystem._Instance._UIManager.CloseTitleMenu();
        TitleSystem._Instance._UIManager.OpenScenarioList();
    }

    private void OnClickAbout()
    {
        PlaySelectSound();
        TitleSystem._Instance._UIManager.CloseTitleMenu();
        TitleSystem._Instance._UIManager.OpenAbout();
    }

    private void OnClickExit()
    {
        PlaySelectSound();
        TitleSystem._Instance.DoExit();
    }

    private void PlaySelectSound()
    {
        SoundManager._Instance.PlaySound(Define._menuSelectSound);
    }
}