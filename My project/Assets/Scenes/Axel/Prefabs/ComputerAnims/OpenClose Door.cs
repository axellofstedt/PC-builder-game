using System.Threading;
using UnityEngine;

public class OpenCloseDoor : MonoBehaviour
{
    float timer = 0.0f;
    Animator animator;
    GameObject chassi;
    bool doorOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        chassi = transform.parent.parent.parent.gameObject;
        animator = GetComponent<Animator>();
    }

    public void interactDoor()
    {
        if (animator.GetBool("isOpen"))
        {
            animator.SetBool("isOpen", false);
            animator.SetTrigger("close");
        }
        else
        {
            animator.SetBool("isOpen", true);
            animator.SetTrigger("open");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(chassi.GetComponent<Selectable>().currentZone.zoneType == ZoneType.Workbench && doorOpen == false)
        {
            interactDoor();
            doorOpen = true;
        }
        if (chassi.GetComponent<Selectable>().currentZone.zoneType != ZoneType.Workbench && doorOpen == true)
        {
            interactDoor();
            doorOpen = false;
        }
        /*
        timer+= Time.deltaTime;
        //temporary call every 3 seconds to test the animation, will be called by the player script when the player interacts with the door
        if (timer >= 3)
        {
            timer = 0;
            interactDoor();
        }
        */
    }
}
