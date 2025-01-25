using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // openDoor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public Vector3 newRotation = new Vector3(0f, 90f, 0f); // Target rotation (0, 90, 0)
    //public Vector3 translationOffset = new Vector3(2f, 0f, 0f); // Move along the x-axis by 2 units

    public void openDoor()
    {
        Vector3 newRotation = new Vector3(0f, 90f, 0f);
        // Set new rotation
        transform.rotation = Quaternion.Euler(newRotation);

        Vector3 translationOffset = new Vector3(0f, 0f, 0.9f);
        // Translate door on the x-axis
        transform.position += translationOffset;
    }
}
