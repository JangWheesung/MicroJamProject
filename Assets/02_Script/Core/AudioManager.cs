using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StartBgm(string name, float volume = 1f)
    {
        AudioClip bgmClip = Resources.Load<AudioClip>($"BGM/{name}");
        if (bgmClip == null)
        {
            Debug.LogError("BGM is not found");
            return;
        }

        bgmSource.clip = bgmClip;
        bgmSource.volume = volume;
        bgmSource.Play();
    }

    public void StartSfx(string name, float volume = 1f)
    {
        AudioClip sfxClip = Resources.Load<AudioClip>($"SFX/{name}");
        if (sfxClip == null)
        {
            Debug.LogError("SFX is not found");
            return;
        }

        var source = PoolingManager.Instance.Pop<AudioSource>(sfxSource.name, Vector2.zero);

        source.clip = sfxClip;
        source.volume = volume;
        source.Play();

        StartCoroutine(SourceDeleteCo(source));
    }

    private IEnumerator SourceDeleteCo(AudioSource source)
    {

        yield return new WaitForSeconds(source.clip.length + 0.3f);

        if (source == null) yield break;

        PoolingManager.Instance.Push(source.gameObject);

    }

}
