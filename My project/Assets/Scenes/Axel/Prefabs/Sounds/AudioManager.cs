using UnityEngine;
using UnityEngine.Audio;
using static Unity.VisualScripting.Member;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [Header("Audio Clips")]
    [SerializeField] public AudioClip musicClip;
    [SerializeField] public AudioClip pickupClip;
    [SerializeField] public AudioClip placeClip;
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer mixer;

    public float currentVolume = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PlayMusic(musicClip);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void Play3DSound(AudioSource localSource, AudioClip clip)
    {
        Debug.Log($"Playing 3D sound: {clip.name} at {localSource.transform.position}");
        if (localSource == null || clip == null)
            return;
        
        localSource.spatialBlend = 1f;
        //localSource.outputAudioMixerGroup = sfxGroup;
        localSource.PlayOneShot(clip);
    }

    // volume: 0–1
    public void SetMusicVolume(float volume)
    {
        Debug.Log($"SetMusicVolume called on: {gameObject.name}");
        //mixer.SetFloat("MusicVolume", ToDecibels(volume));
        musicSource.volume = volume;
        currentVolume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        mixer.SetFloat("SFXVolume", ToDecibels(volume));
    }

    private float ToDecibels(float volume)
    {
        return Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
    }
}