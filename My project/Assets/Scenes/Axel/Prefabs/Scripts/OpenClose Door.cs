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
    [SerializeField] DoorSoundEffects doorSoundEffects;
    [SerializeField] Animator animator;
    //float timer = 0.0f;
    GameObject part;
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
            animator.SetBool("isOpen", false);
            doorSoundEffects.PlayCloseSound();
            animator.SetTrigger("close");
        }
        else
        {
            animator.SetBool("isOpen", true);
            doorSoundEffects.PlayOpenSound();
            animator.SetTrigger("open");
        }
    }

    public void closeDoor()
    {
        if (animator.GetBool("isOpen"))
        {
            animator.SetBool("isOpen", false);
            doorSoundEffects.PlayCloseSound();
            animator.SetTrigger("close");
        }
    }

    public void openDoor()
    {
        if (!animator.GetBool("isOpen"))
        {
            animator.SetBool("isOpen", true);
            doorSoundEffects.PlayOpenSound();
            animator.SetTrigger("open");
        }
    }
}
