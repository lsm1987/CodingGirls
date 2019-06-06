using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 제작 정보 창
/// </summary>
public class UIAbout : UIWindow
{
    [SerializeField]
    private Button _btnClose = null;

    private void Awake()
    {
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

    private void OnClickBack()
    {
        PlaySelectSound();
        TitleSystem._Instance._UIManager.CloseAbout();
        TitleSystem._Instance._UIManager.OpenTitleMenu();
    }

    private void PlaySelectSound()
    {
        SoundManager._Instance.PlaySound(Define._menuSelectSound);
    }
}
