using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOverlay : MonoBehaviour
{
    [SerializeField]
    private Image _sprite;
    private GameObject _go;
    private GameObject _Go { get { if (_go == null) { _go = gameObject; } return _go; } }
    private Coroutine _fadeCoroutine;

    /// <summary>
    /// 어두워지기
    /// </summary>
    public void DoFadeOut(float duration)
    {
        StopFade();
        _Go.SetActive(true);

        if (duration != 0.0f)
        {
            _fadeCoroutine = StartCoroutine(Fade(true, duration));
        }
        else
        {
            Color color = _sprite.color;
            color.a = 1.0f;
            _sprite.color = color;
        }
    }

    /// <summary>
    /// 밝아지기
    /// </summary>
    public void DoFadeIn(float duration)
    {
        StopFade();

        if (duration != 0.0f)
        {
            _Go.SetActive(true);
            _fadeCoroutine = StartCoroutine(Fade(false, duration));
        }
        else
        {
            _Go.SetActive(false);
        }
    }

    private IEnumerator Fade(bool isOut, float duration)
    {
        float startTime = Time.time;
        float startAlpha = (isOut ? 0.0f : 1.0f);
        float endAlpha = (isOut ? 1.0f : 0.0f);
        Color color = _sprite.color;

        while (Time.time - startTime - duration < 0)
        {
            float elapsed = (Time.time - startTime);
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            _sprite.color = color;
            yield return null;
        }

        color.a = endAlpha;
        _sprite.color = color;
        if (!isOut)
        {
            _Go.SetActive(false);
        }
        _fadeCoroutine = null;
    }

    private void StopFade()
    {
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = null;
        }
    }

    public bool IsFading()
    {
        return (_fadeCoroutine != null);
    }
}