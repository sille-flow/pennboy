using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerLose : MonoBehaviour
{
    public GameObject door0;
    private OpenDoor openDoorScript;

    public GameObject Ben;
    public GameObject GameOverCanvas;
    public int checker;
    private PlayerMovement playerMovement;

    void Start()
    {
        openDoorScript = door0.GetComponent<OpenDoor>();
    }

    private void OnTriggerEnter(Collider p)
    {

        Debug.Log(checker);
        Debug.Log(GameData.zeroOne);
        if (checker != GameData.zeroOne) {
            return;
        }

        if (p.CompareTag("Player"))
        {
            openDoorScript = door0.GetComponent<OpenDoor>();

            if (GameData.wrongDoorChosen)
            //if (true)
            {
                Ben.SetActive(true);
                

                // TODO: NOW WAIT FOR 1 SEC
                StartCoroutine(WaitBeforePause(p));

                //Time.timeScale = 0f;
                //playerMovement = p.GetComponent<PlayerMovement>();
                //if (playerMovement != null)
                //    playerMovement.enabled = false;

                //Cursor.visible = true;
                //Cursor.lockState = CursorLockMode.None;


            }
            


        }
    }

    public void Restart()
    {
        GameData.firstPuzzleSolved = false;
        GameData.goToMemory = false;
        Time.timeScale = 1f;

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator WaitBeforePause(Collider player)
    {
        yield return new WaitForSecondsRealtime(0.2f);

        Time.timeScale = 0f;
        GameOverCanvas.SetActive(true);
        playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
            playerMovement.enabled = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
