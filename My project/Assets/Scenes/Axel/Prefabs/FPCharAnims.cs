using System;
using UnityEngine;

public class FPCharAnims : MonoBehaviour
{
    private Animator animator;
    private bool isRunning = false;
    private bool Wdown = false;
    private bool Adown = false;
    private bool Sdown = false;
    private bool Ddown = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void interactAnimation()
    {
        animator.SetTrigger("interact");
        return;
    }
    public void happyAnimation()
    {
        animator.SetTrigger("happy");
        return;
    }
    public void sadAnimation()
    {
        animator.SetTrigger("sad");
        return;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Wdown = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Adown = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Sdown = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Ddown = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            Wdown = false;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            Adown = false;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            Sdown = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            Ddown = false;
        }
        if ((Wdown || Adown || Sdown || Ddown) && !isRunning)
        {
            isRunning = true;
            animator.SetTrigger("run");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRunning = false;
            animator.SetTrigger("jump");
        }
        if(!Wdown && !Adown && !Sdown && !Ddown && isRunning)
        {
            isRunning = false;
            animator.SetTrigger("idle");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactAnimation();
            animator.SetTrigger("idle");
        }
        if (Input.GetKeyDown(KeyCode.Keypad9)){
            happyAnimation();
        }
        if (Input.GetKeyDown(KeyCode.Keypad8)){
            sadAnimation();
        }
    }
}
