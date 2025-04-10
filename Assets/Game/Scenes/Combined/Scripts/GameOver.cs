using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameOverC : MonoBehaviour
{

    public GameObject defaultSelection;

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != defaultSelection)
        {
            EventSystem.current.SetSelectedGameObject(defaultSelection);
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("CombinedScene");
    }
}
