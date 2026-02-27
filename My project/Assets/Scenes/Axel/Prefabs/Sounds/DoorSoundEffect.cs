using UnityEngine;

public class DoorSoundEffects : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip closeClip;

    public void PlayOpenSound()
    {
        audioSource.PlayOneShot(openClip);
    }

    public void PlayCloseSound()
    {
        audioSource.PlayOneShot(closeClip);
    }
}
