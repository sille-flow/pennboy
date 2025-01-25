using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToPuzzle : MonoBehaviour
{
    public GameObject Controller;

    private void OnTriggerEnter(Collider p)
    {
        // Check if the object entering the trigger is the player
        if (p.CompareTag("Player"))
        {
            if (!GameData.firstPuzzleSolved && !GameData.goToMemory) {
                Cursor.lockState = CursorLockMode.None;
                GameData.firstPuzzleSolved = true;

                Debug.Log("test1");


                // switch to Puzzle
                SceneManager.LoadScene("SlidingPuzzle");

            }
            else if (!GameData.secondPuzzleSolved && GameData.goToMemory)
            {
                Cursor.lockState = CursorLockMode.None;
                GameData.secondPuzzleSolved = true;

                Debug.Log("test2");

                // switch to second puzzle
                SceneManager.LoadScene("SlidingPuzzle");
                // SceneManager.LoadScene("Memory Game");

            } else
            {
                GameData.goToMemory = true;
                Debug.Log("test3");
            }


            
        }
    }
}
