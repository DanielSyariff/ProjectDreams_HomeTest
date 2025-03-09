using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip worldBGM;
    public AudioClip battleBGM;

    [Header("Audio Source")]
    public AudioSource bgmSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayBGM(AudioClip bgmClip)
    {
        if (bgmSource.clip == bgmClip && bgmSource.isPlaying) return;

        bgmSource.clip = bgmClip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void WorldMusic()
    {
        StopBGM();
        PlayBGM(worldBGM);
    }
    public void BattleMusic()
    {
        StopBGM();
        PlayBGM(battleBGM);
    }
}
