using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleSystem : MonoBehaviour
{
    public static TitleSystem _Instance { get; private set; }
    public TitleUIManager _UIManager { get; private set; }
    private const float _fadeDuration = 1.5f;

    private void Initialize()
    {
        _Instance = this;
        SoundManager.Create();
        InitializeUIManager();
    }

    private void Clear()
    {
        if (SoundManager._Instance != null)
        {
            SoundManager._Instance.StopBGM();
            SoundManager._Instance.ClearLoadedAudioClip();
        }
        _Instance = null;
    }

    private void Awake()
    {
        Initialize();
        StartCoroutine(SceneStartSequence());
    }

    private void OnDestroy()
    {
        Clear();
    }

    private void InitializeUIManager()
    {
        _UIManager = FindObjectOfType<TitleUIManager>();
        _UIManager.Initailize();
    }

    /// <summary>
    /// 씬 시작 시퀀스
    /// </summary>
    private IEnumerator SceneStartSequence()
    {
        // 로딩
        SoundManager._Instance.LoadBGM(Define._titleBGM);
        SoundManager._Instance.LoadSound(Define._menuSelectSound);
        yield return new WaitForSeconds(0.3f);

        _UIManager._FadeOverlay.DoFadeIn(_fadeDuration);
        yield return new WaitForSeconds(0.5f);
        SoundManager._Instance.PlayBGM(Define._titleBGM);
    }

    public void DoGameStartSequence()
    {
        StartCoroutine(GameStartSequence());
    }

    private IEnumerator GameStartSequence()
    {
        _UIManager._FadeOverlay.DoFadeOut(_fadeDuration);
        yield return new WaitForSeconds(_fadeDuration);
        SceneManager.LoadScene(Define.SceneName._game);
    }

    public void DoExit()
    {
        Application.Quit();
    }
}