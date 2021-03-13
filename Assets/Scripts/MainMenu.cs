using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject StartMenuObject;
    public GameObject MainMenuObject;
    public void Play()
    {
        MainMenuObject.SetActive(false);
        StartMenuObject.SetActive(true);
    }
    public void EasyDifficulty()
    {
        SceneManager.LoadScene("EasyGame");
    }
    public void MediumDifficulty()
    {
        SceneManager.LoadScene("MediumGame");
    }
    public void HardDifficulty()
    {
        SceneManager.LoadScene("HardGame");
    }
    public void Back()
    {
        StartMenuObject.SetActive(false);
        MainMenuObject.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
