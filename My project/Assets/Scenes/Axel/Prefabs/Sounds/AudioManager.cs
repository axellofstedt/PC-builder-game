using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static Unity.VisualScripting.Member;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource sfxSource;

    [Header("Audio Clips")]
    [SerializeField] public AudioClip musicClip;
    [SerializeField] public AudioClip pickupClip;
    [SerializeField] public AudioClip placeClip;
    [SerializeField] public AudioClip starPling;

    [SerializeField] public List<AudioClip> partyPhilSFX;

    [Header("Audio Mixer")]
    [SerializeField] public AudioMixer mixer;

    //public float currentVolume = 1f;
    public float musicVolume = 1f;
    public float sfxVolume = 1f;
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
        localSource.outputAudioMixerGroup =
      mixer.FindMatchingGroups("SFX")[0];
        localSource.PlayOneShot(clip);
    }

    // volume: 0�1
    public void SetMusicVolume(float volume)
    {
        Debug.Log($"SetMusicVolume called on: {gameObject.name}");
        musicVolume = volume;
        mixer.SetFloat("MusicVolume", ToDecibels(volume));  
    //mixer.SetFloat("MusicVolume", ToDecibels(volume));
    //musicSource.volume = volume;
    //currentVolume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        float db = ToDecibels(volume);
        Debug.Log($"SFX volume slider: {volume} -> {db} dB");
        mixer.SetFloat("SFXVolume", db);
        //mixer.SetFloat("SFXVolume", ToDecibels(volume));
    }

    private float ToDecibels(float volume)
    {
        return Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
    }
}