using UnityEngine;

public class Load : MonoBehaviour
{
    void Awake()
    {
        if (SaveManager.Instance.loadBool == true)
        {
            SaveManager.Instance.LoadGame();
        }
        
    }
}
