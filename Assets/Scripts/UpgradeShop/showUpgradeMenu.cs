using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showUpgradeMenu : MonoBehaviour
{

    public GameObject buyMenuUI;
    public static bool buyMenuActive;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (buyMenuActive)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        buyMenuUI.SetActive(false);
        Time.timeScale = 1f;
        buyMenuActive = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        buyMenuUI.SetActive(true);
        Time.timeScale = 0f;
        buyMenuActive = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
