using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShadowPitScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            var player = other.gameObject;
            var mesh = player.transform.Find("Mesh");
            Destroy(mesh.gameObject);
            SceneManager.LoadScene("Level1Test");
        }
    }
}
