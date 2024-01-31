using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Howtoplay1()
    {
        SceneManager.LoadSceneAsync(3);
    }

    public void Howtoplay2()
    {
        SceneManager.LoadSceneAsync(4);
    }
}
