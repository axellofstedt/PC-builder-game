using UnityEngine;

public class PlayerSoundEffects : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pickupClip;
    [SerializeField] private AudioClip placeClip;

    public void PlayPickUpSound()
    {
        audioSource.PlayOneShot(pickupClip);
    }

    public void PlayPlaceSound()
    {
        audioSource.PlayOneShot(placeClip);
    }
}
