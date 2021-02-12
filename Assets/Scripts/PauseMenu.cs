using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject pauseMenuUI;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        //Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        IsPaused = false;
    }
    void Pause()
    {
        //Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        IsPaused = true;
    }
    public void QuitToMenu()
    {
        IsPaused = false;
        SceneManager.LoadScene("Main Menu");
    }
    public void QuitToDesktop()
    {
        IsPaused = false;
        Application.Quit();
    }
}
