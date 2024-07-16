using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenuPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(mainMenuPanel.activeInHierarchy == false)
            {
                openMenu();
            }
            else
            {
                closeMenu();
            }
        }
    }

    public void openMenu()
    {
        mainMenuPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void closeMenu()
    {
        mainMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
