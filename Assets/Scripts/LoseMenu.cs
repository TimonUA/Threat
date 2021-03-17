using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    public void TryAgein()
    {
        Game.IsEnd = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
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
