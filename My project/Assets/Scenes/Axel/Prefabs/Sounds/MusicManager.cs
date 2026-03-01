using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        musicSource.Play();
    }
}