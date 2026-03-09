using UnityEngine;
using TMPro;

public class TutorialScript : MonoBehaviour
{
    public static TutorialScript Instance;
    public TextMeshProUGUI objectiveText;
    public CheckoutMode checkoutMode;
    public PlacementZone placementZone;

    private int currentObjective = 0;

    public bool tutorialCompleted = false;

    private bool w = false;
    private bool a = false;
    private bool s = false;
    private bool d = false;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if(tutorialCompleted==true || DayNightManager.Instance.getCurrentDay() != 1) { 
            objectiveText.gameObject.SetActive(false); 
            tutorialCompleted = true;
        }
            
    }

    void Update()
    {
        if (DayNightManager.Instance.getCurrentDay() == 1) { CheckObjectives(); }

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
            objectiveText.text = "Assemble the PC at the building table. If you have a wrong part, DO NOT PRESS DONE!";
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
            case 3:
                {
                    if (Objective3())
                    {
                        Next();
                        break;
                    }
                    break;
                }
            case 4:
                {
                    if (Objective4())
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
            // Debug.Log(placementZone.NumberOfFreeSlots());
            if (placementZone.NumberOfFreeSlots() == 0)
            { 
                return true; 
            }
            return false;
        }

        bool Objective3()
        {
            if (SelectionManager.Instance.GetNumberPlaced() >= 8)
            {
                return true;
            }
            return false;
        }

        bool Objective4()
        {
            if (SelectionManager.Instance.GetNumberPlaced() == 9)
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