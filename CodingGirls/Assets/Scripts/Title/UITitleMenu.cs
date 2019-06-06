using UnityEngine;
using UnityEngine.UI;

public class UITitleMenu : UIWindow
{
    [SerializeField]
    private Button _btnStart = null;
    [SerializeField]
    private Button _btnAbout = null;
    [SerializeField]
    private Button _btnExit = null;

    private void Awake()
    {
        _btnStart.onClick.AddListener(OnClickStart);
        _btnAbout.onClick.AddListener(OnClickAbout);

        if (IsNeedExitMenu())
        {
            _btnExit.onClick.AddListener(OnClickExit);
        }
        else
        {
            _btnExit.gameObject.SetActive(false);
        }
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

    private bool IsNeedExitMenu()
    {
        return Application.platform != RuntimePlatform.IPhonePlayer;
    }
}