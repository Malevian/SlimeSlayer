using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    public void quitGame()
    {
        Debug.Log("Quit the game");
        Application.Quit();
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
