using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _bgmSource;
    [SerializeField]
    private AudioSource _soundSource;
    public static SoundManager _Instance { get; private set; }
    private Dictionary<string, AudioClip> _loadedClip = new Dictionary<string, AudioClip>();

    public static void Create()
    {
        if (_Instance != null)
        {
            // 이미 있는 상태로 생성시도해도 오류 아님
            return;
        }

        SoundManager prefab = Resources.Load<SoundManager>(Define._soundManagerPrefabPath);
        if (prefab == null)
        {
            Debug.LogError("[SoundManager.Create - Invalid prefab path]" + Define._soundManagerPrefabPath);
            return;
        }

        _Instance = GameObject.Instantiate<SoundManager>(prefab);
        DontDestroyOnLoad(_Instance.gameObject);
    }

    public static void Clear()
    {
        if (_Instance != null)
        {
            GameObject.Destroy(_Instance);
        }        
        _Instance = null;
    }

    private AudioClip LoadAudioClip(string fullPath)
    {
        AudioClip clip = null;
        if (_loadedClip.TryGetValue(fullPath, out clip))
        {
            return clip;
        }

        clip = Resources.Load<AudioClip>(fullPath);
        if (clip == null)
        {
            Debug.LogError("[SoundManager.LoadAudioClip.InvalidPath]" + fullPath);
            return null;
        }

        _loadedClip.Add(fullPath, clip);
        return clip;
    }

    private static string GetBGMFullPath(string path)
    {
        return Define._bgmRoot + "/" + path;
    }

    public void LoadBGM(string path)
    {
        LoadAudioClip(GetBGMFullPath(path));
    }

    public void PlayBGM(string path)
    {
        AudioClip clip = LoadAudioClip(GetBGMFullPath(path));
        if (clip == null)
        {
            return;
        }
        _bgmSource.clip = clip;
        _bgmSource.Play();
    }

    public void StopBGM()
    {
        _bgmSource.Stop();
    }

    private static string GetSoundFullPath(string path)
    {
        return Define._soundRoot + "/" + path;
    }

    public void LoadSound(string path)
    {
        LoadAudioClip(GetSoundFullPath(path));
    }

    public void PlaySound(string path)
    {
        AudioClip clip = LoadAudioClip(GetSoundFullPath(path));
        if (clip == null)
        {
            return;
        }
        _soundSource.PlayOneShot(clip);
    }

    public void ClearLoadedAudioClip()
    {
        _loadedClip.Clear();
    }
}