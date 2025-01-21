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

    public void LoadLevel3()
    {
        SceneManager.LoadScene(9);
    }

    public void LoadLevel4()
    {
        SceneManager.LoadScene(10);
    }

    public void LoadLevel5()
    {
        SceneManager.LoadScene(11);
    }

    public void LoadLevel7()
    {
        SceneManager.LoadScene(12);
    }

    public void LoadLevel6()
    {
        SceneManager.LoadScene(13);
    }

    public void Settings()
    {
        SceneManager.LoadScene(14);
    }

    public void restartLevel() {
        int returnTo = PlayerPrefs.GetInt("returnTo");
        //Debug.Log(returnTo);
        SceneManager.LoadScene(returnTo);
    }
}
