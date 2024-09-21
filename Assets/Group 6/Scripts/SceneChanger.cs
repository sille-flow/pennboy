using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void LoadTutorialScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene(6);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(7);
    }

    public void LoadHowToPlay()
    {
        SceneManager.LoadScene(8);
    }
}
