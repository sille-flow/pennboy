using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMonsterScript : MonoBehaviour
{
    //shadow monster follows a temple-run monster type behavior
    //three states: off-screen, side-screen, on player
    //advances state when player is slowed down by obstacle OR player velocity falls below threshold for certain amount of time
    //lowers state when player dodges obstacles for certain amount of time AND player velocity exceeds threshold for certain amount of time
    //off-screen, no behavior
    //side-scree, launches foxfires
    //on-player, kills player

    [SerializeField] GameObject foxfire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
