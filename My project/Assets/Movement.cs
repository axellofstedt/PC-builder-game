using System.Runtime.CompilerServices;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Animator animator;
    private Transform charTransform;
    private Rigidbody cameraRigidbody;
    private bool W = false;
    private bool A = false;
    private bool S = false;
    private bool D = false;
    private bool isIdle = false;
    private bool isRunning = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        charTransform = GetComponent<Transform>();
        cameraRigidbody = charTransform.parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("isSprinting", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("isSprinting", false);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            W = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            A = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            S = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            D = true;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            W = false;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            A = false;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            S = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            D = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && cameraRigidbody.linearVelocity.y == 0)
        {
            animator.SetTrigger("jump");
        }
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) && !isRunning)
        {
            isRunning = true;
            isIdle = false;
    
            animator.SetTrigger("run");
        } 
        if (!W && !A && !S && !D && !isIdle)
        {
            isIdle = true;
            isRunning = false;
            animator.SetTrigger("idle");
        }

        

        //This one is janky with the camera
        /*if (cameraRigidbody.linearVelocity.y < -0.1f)
        {
            animator.SetTrigger("fall");
        }*/
    }
}