using UnityEngine;

public class BotAnimation : MonoBehaviour
{
    Animator animator;
    bool isWalking = false;
    bool isIdle = false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartWalking()
    {
        if (isWalking) return;

        isWalking = true;
        animator.SetTrigger("run");
    }

    public void StopWalking()
    {
        if (!isWalking) return;

        isWalking = false;
        animator.SetTrigger("idle");
    }

    public void StartIdle()
    {
        if (isIdle) return;

        isIdle = true;
        animator.SetTrigger("idle");
    }
    public void StopIdle()
    {
        if (!isIdle) return;

        isIdle = false;
        animator.SetTrigger("run");
    }

}
