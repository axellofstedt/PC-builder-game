using UnityEngine;
using TMPro;

public class TutorialScript : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;
    public CheckoutMode checkoutMode;

    private int currentObjective = 0;

    private bool w = false;
    private bool a = false;
    private bool s = false;
    private bool d = false;

    void Start()
    {
        ShowObjective();
    }

    void Update()
    {
        CheckObjectives();
        
    }

    void ShowObjective()
    {
        if (currentObjective == 0)
            objectiveText.text = "Move with WASD, press all the buttons.";

        else if (currentObjective == 1)
            objectiveText.text = "Behind the counter, take an order from a customer.";

        else if (currentObjective == 2)
            objectiveText.text = "Go to the storage room and collect the order!";
        else if (currentObjective == 3)
            objectiveText.text = "Assemble the PC at the building table. If you forgot a part, DO NOT PRESS DONE!";
        else if (currentObjective == 4)
            objectiveText.text = "Click done and go back to the counte. Return the PC!";

        else
            objectiveText.text = "";
    }

    void CheckObjectives()
    {
        switch (currentObjective)
        {
            case 0:
                {
                    if (Objective0())
                    {
                        Next();
                        break;
                    }
                    break;
                }
            case 1:
                {
                    if (Objective1())
                    {
                        Next();
                        break;
                    }
                    break;
                }
            case 2:
                {
                    if (Objective2())
                    {
                        Next();
                        break;
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }

        bool Objective0()
        {
            if (Input.GetKey(KeyCode.W))
            { w = true; }
            if (Input.GetKey(KeyCode.A))
            { a = true; }
            if (Input.GetKey(KeyCode.S))
            { s = true; }
            if (Input.GetKey(KeyCode.D))
            { d = true; }
            if (w && a && s && d)
            {
                return true;
            }
            return false;
        }

         bool Objective1()
        {
            if (checkoutMode.hasTakenOrder)
            { return true; }
            return false;
        }

        bool Objective2()
        {
            if (checkoutMode.hasTakenOrder)
            { 
                return true; 
            }
            return false;
        }

        void Next()
        {
            currentObjective++;
            ShowObjective();
        }
    }
}