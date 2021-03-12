using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public void QuitToMenu()
    {
        Game.IsEnd = false;
        SceneManager.LoadScene("Main Menu");
    }
    public void QuitToDesktop()
    {
        Game.IsEnd = false;
        Application.Quit();
    }
}
