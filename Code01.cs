using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Code01 : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Setting()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitPage()
    {
        SceneManager.LoadScene(3);
    }

    public void StartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Menu()
    {
        SceneManager.LoadScene(4);
    }

    public void Quit()
    {
        Application.Quit();
    }
}