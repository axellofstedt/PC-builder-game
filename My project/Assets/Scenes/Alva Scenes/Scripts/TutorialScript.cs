using UnityEngine;
using TMPro;

public class TutorialScript : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;

    private int currentObjective = 0;

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
            objectiveText.text = "Sprint with SHIFT";

        else
            objectiveText.text = "";
    }

    void CheckObjectives()
    {   switch( currentObjective)
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
        }
    }

    private bool Objective0()
    {
        bool w = false;
        bool a = false;
        bool s = false;
        bool d = false;
        if(Input.GetKey(KeyCode.W))
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

    private bool Objective1()
    {
        if(Input.GetKey(KeyCode.LeftShift))
            { return true; }
        return false; 
    }

    private bool Objective2()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        { return true; }
        return false;
    }

    private void Next()
    {
        currentObjective++;
        ShowObjective();
    }
}