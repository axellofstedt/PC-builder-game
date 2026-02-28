using System.Threading;
using UnityEngine;


public enum DoorType
{
    Regular,
    Chassi,
    CPU,
    FrontDoor
}

public class OpenCloseDoor : MonoBehaviour
{
    [SerializeField] DoorType doorType;
    [SerializeField] Animator animator;
    [SerializeField] AudioClip openSound;
    [SerializeField] AudioClip closeSound;
    [SerializeField] AudioSource doorSource;
    //float timer = 0.0f;
    bool doorOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (animator==null)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void interactDoor()
    {
        if (animator.GetBool("isOpen"))
        {
            AudioManager.Instance.Play3DSound(doorSource, closeSound);
            animator.SetTrigger("close");
            animator.SetBool("isOpen", false);
            return;
        }
        else
        {
            AudioManager.Instance.Play3DSound(doorSource, openSound);
            animator.SetTrigger("open");
            animator.SetBool("isOpen", true);
            return;
        }
    }

    public void closeDoor()
    {
        if (animator.GetBool("isOpen"))
        {
            AudioManager.Instance.Play3DSound(doorSource, closeSound);
            animator.SetTrigger("close");
            animator.SetBool("isOpen", false);
        }
    }

    public void openDoor()
    {
        if (!animator.GetBool("isOpen"))
        {
            AudioManager.Instance.Play3DSound(doorSource, openSound);
            animator.SetTrigger("open");
            animator.SetBool("isOpen", true);
        }
    }
}
