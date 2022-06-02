using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioScriptableObject sfxSO;
    public AudioScriptableObject bgmSO;

    AudioSource source;
    AudioSource sfxSource;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        source = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();
    }

    public void PlaySfxOneShot(string name)
    {
        var data = sfxSO.audioList.Find(x => x.name == name);
        if (data != null)
        {
            source.PlayOneShot(data.clip);
        }
        else
        {
            Debug.Log($"[AudioManager] SFX Not found: {name}");
        }
    }

    public void PlaySfx(string name)
    {
        var data = sfxSO.audioList.Find(x => x.name == name);
        if (data != null)
        {
            sfxSource.Stop();
            sfxSource.clip = data.clip;
            sfxSource.volume = 1;
            sfxSource.Play();
        }
        else
        {
            Debug.Log($"[AudioManager] SFX Not found: {name}");
        }
    }

    public void StopSfx()
    {
        sfxSource.Stop();
    }

    public void PlayBGM(string name, float volume = 1f)
    {
        var data = bgmSO.audioList.Find(x => x.name == name);
        if (data != null)
        {
            source.Stop();
            source.clip = data.clip;
            source.volume = volume;
            source.loop = true;
            source.Play();
        }
        else
        {
            Debug.Log($"[AudioManager] BGM Not found: {name}");
        }
    }

    public void StopBGM()
    {
        source.Stop();
    }
}