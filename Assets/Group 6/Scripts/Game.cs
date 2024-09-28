using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private int budget = 1000;
    [SerializeField] private int winScene = 3;
    [SerializeField] private int loseScene = 4;
    [SerializeField] private HUD hud;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        hud.updateBudget(budget);
    }

    // Update is called once per frame
    void Update()
    {
        if (budget <= 0) {
            SceneManager.LoadScene(loseScene);
            Cursor.lockState = Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void reduceBudget(int damage) {
        budget -= damage;
        hud.updateBudget(budget);
        print(budget);
    }

    public void win() {
        SceneManager.LoadScene(winScene);
    }
}
