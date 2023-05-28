using UnityEngine;
public class SoundManager : MonoBehaviour
{
    // BGM用、SE用
    [SerializeField]
    public AudioSource bgmAudioSource;
    [SerializeField]
    AudioSource seAudioSource;

    //音量いじる場所
    public float BgmVolume
    {
        get
        {
            return bgmAudioSource.volume;
        }
        set
        {
            bgmAudioSource.volume = Mathf.Clamp01(value);
        }
    }

    public float SeVolume
    {
        get
        {
            return seAudioSource.volume;
        }
        set
        {
            seAudioSource.volume = Mathf.Clamp01(value);
        }
    }

    //消さずにManagerをとっといておくもの
    void Start()
    {
        GameObject soundManager = CheckOtherSoundManager();
        bool checkResult = soundManager != null && soundManager != gameObject;
        if (checkResult)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    GameObject CheckOtherSoundManager()
    {
        return GameObject.FindGameObjectWithTag("SoundManager");
    }

    //再生用
    public void PlayBgm(AudioClip clip)
    {
        bgmAudioSource.clip = clip;
        if (clip == null)
        {
            return;
        }
        bgmAudioSource.Play();
    }
    public void PlaySe(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }
        seAudioSource.PlayOneShot(clip);
    }
}