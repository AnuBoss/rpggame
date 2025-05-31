using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] bgm;
    public AudioSource[] BGM { get { return bgm; } }

    [SerializeField]
    private AudioSource[] sfx;
    public AudioSource[] SFX { get { return sfx; } }

    [SerializeField]
    private AudioMixer audioMixer;

    public static AudioManager instance;

    void  Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayBGM(0);//play.main.theme
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StopAllBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }

    public void PlayBGM(int i)
    {
        if (i < BGM.Length && !BGM[i].isPlaying)
        {
            StopAllBGM();
            BGM[i].PlayDelayed(2f);
        }
    }

    public void PlaySFX(int i)
    {
        if (i < sfx.Length && !sfx[i].isPlaying)
        {
            sfx[i].Play();
        }
    }
}
