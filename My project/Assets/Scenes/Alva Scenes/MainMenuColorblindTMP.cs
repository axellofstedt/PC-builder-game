using UnityEngine;
using TMPro;

public class MainMenuColorblindTMP : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    void Start()
    {
        // Lägg till event listener
        dropdown.onValueChanged.AddListener(OnDropdownChanged);

        // Läs tidigare sparat värde (default 0 = Normal)
        int savedIndex = PlayerPrefs.GetInt("ColorblindMode", 0);
        dropdown.value = savedIndex;
    }

    public void OnDropdownChanged(int index)
    {
        // Spara valet
        PlayerPrefs.SetInt("ColorblindMode", index);
        PlayerPrefs.Save();
    }
}