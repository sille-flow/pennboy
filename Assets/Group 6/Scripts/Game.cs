using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField]
    private int budget = 1000;
    [SerializeField]
    private int winScene = 3;
    [SerializeField]
    private int loseScene = 4;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (budget == 0) {
            SceneManager.LoadScene(loseScene);
        }
    }

    public void reduceBudget(int damage) {
        budget -= damage;
    }

    public void win() {
        SceneManager.LoadScene(winScene);
    }
}
