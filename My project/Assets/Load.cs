using UnityEngine;

public class Load : MonoBehaviour
{
    void Start()
    {
        if (SaveManager.Instance.loadBool == true)
        {
            SaveManager.Instance.LoadGame();
        }
        
    }
}
