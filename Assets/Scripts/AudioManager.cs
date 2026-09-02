using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource seSource;

    [Header("BGM Clips")]
    [SerializeField] private AudioClip mainBGM;

    [Header("SE Clips")]
    [SerializeField] private AudioClip collectSE;
    [SerializeField] private AudioClip gameStartSE;
    [SerializeField] private AudioClip timeUpSE;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (mainBGM != null)
        {
            playBGM(mainBGM);
        }
    }

    public void playBGM(AudioClip clip)
    {
        if (bgmSource == null || clip == null)
        {
            return;
        }

        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void stopBGM()
    {
        if (bgmSource != null)
        {
            bgmSource.Stop();
        }
    }

    public void playSE(AudioClip clip)
    {
        if (seSource == null || clip == null)
        {
            return;
        }

        seSource.PlayOneShot(clip);
    }

    public void playCollectSE()
    {
        playSE(collectSE);
    }

    public void playTimeUpSE()
    {
        playSE(timeUpSE);
    }
}
