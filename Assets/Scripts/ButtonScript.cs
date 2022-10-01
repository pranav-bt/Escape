using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void Level1()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Level2()
    {
        SceneManager.LoadScene("SampleScene 1");
    }

    public void RestartScene()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
