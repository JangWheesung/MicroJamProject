using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    public AudioSource BgmSource => bgmSource;
    public AudioSource SfxSource => sfxSource;

    [SerializeField] private List<AudioData> bgmClips;
    [SerializeField] private List<AudioData> sfxClips;

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
    public void StartBgm(string name)
    {
        AudioData data = bgmClips.Find(x => x.audioName == name);
        if (ReferenceEquals(data, null)) return;

        bgmSource.clip = data.audioClip;
        bgmSource.Play();
    }

    public void StartSfx(string name)
    {
        Debug.Log(name);
        AudioData data = sfxClips.Find(x => x.audioName == name);
        if (ReferenceEquals(data, null)) return;

        var source = PoolingManager.Instance.Pop<AudioSource>(sfxSource.name, Vector2.zero);

        source.clip = data.audioClip;
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
