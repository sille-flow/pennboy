using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public List<GameObject> doors;
    // Start is called before the first frame update

    

    void Start()
    {

        // initial setup
        for (int i = 0; i < 6; i++)
        {
            GameObject door = doors[i];
            door.transform.Translate(-1 * door.transform.right, Space.World);
            if (i == 0 || i == 1 || i == 2)
            {
                door.transform.Rotate(Vector3.up, 60f);
            }
            else
            {
                door.transform.Rotate(Vector3.up, -60f);
            }

            door.transform.Translate(1 * door.transform.right, Space.World);
        }



        // choose door for B
        int[] possibleValues = { 0, 1, 3, 4 };
        int randomIndex = Random.Range(0, possibleValues.Length);
        int randomValue = possibleValues[randomIndex];

        Debug.Log("Random value: " + randomValue);

        int benDoor = randomValue; // 0-5

        if (benDoor == 1 || benDoor == 4)
        {
            GameData.zeroOne = 0;
        }
        else if (benDoor == 0 || benDoor == 3)
        {
            GameData.zeroOne = 1;
        }
        else {
            GameData.zeroOne = -1;
        }

        GameData.wrongDoorChosen = true;
        
        // see which doors were chosen
        for (int i = 0; i < 3; i++) {

            int chosenDoorNum = GameData.doorsPicked[i] - 1;
            Debug.Log("doors chosen " + chosenDoorNum);

            // in the range 0 to 5
            if (chosenDoorNum != -1 && chosenDoorNum < doors.Count) {


                // CLOSE DOORS
                GameObject door = doors[chosenDoorNum];
                door.transform.Translate(-1 * door.transform.right, Space.World);
                if (chosenDoorNum == 0 || chosenDoorNum == 1 || chosenDoorNum == 2)
                {
                    door.transform.Rotate(Vector3.up, -60f);
                }
                else {
                    door.transform.Rotate(Vector3.up, 60f);
                }
                
                door.transform.Translate(1 * door.transform.right, Space.World);

                Renderer renderer = doors[chosenDoorNum].GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = Color.blue;
                }

            }

            if (chosenDoorNum == benDoor) {

                GameData.wrongDoorChosen = false;
            }
        } // end of loop
        
}
        
    

    void openDoor(GameObject door)
    {

        //door.transform.Translate(-1 * door.transform.right, Space.World);
        //door.transform.Rotate(Vector3.up, 90f);
        //door.transform.Translate(1 * door.transform.right, Space.World);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
