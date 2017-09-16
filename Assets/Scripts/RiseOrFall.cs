using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------------------------------------------------------------------------------------------------------------------------------
//
// Authors: Nicholas Teese
// Date Created: 04/08/2017
// Description: Handles the doors and spikes within a scene, when the player interacts with a button the object in question will rise or sink
//
//-------------------------------------------------------------------------------------------------------------------------------------------------------------

public class RiseOrFall : MonoBehaviour
{
    // Speed at which the platforms fall
    public float moveSpeed;
    // x* Second countdown for the player to move before platform collapses
    private float timer;
    // Bool to trigger the rise/fall of object
    public bool Sink;

    private float initPos;
    private float lowerPos;


    // Use Awake for initialising
    void Awake()
    {
        timer = 0;
        initPos = transform.position.y;
        lowerPos = transform.position.y - 10.0f; //transform.position.y - transform.localScale.y * 2.0f;
    }

    // Update scene
    void Update()
    {
        if (transform.tag == "Door")
        {
            if (Sink)
            {
                if (transform.position.y > lowerPos)
                    transform.position -= Vector3.up * moveSpeed * Time.deltaTime;
                // Lower the door into the ground
            }
            else
            {
                if (transform.position.y < initPos)
                    transform.position += Vector3.up * moveSpeed * Time.deltaTime;
                // Return the door back to it's original position
            }
        }
    }
}