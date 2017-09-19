using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftManager : MonoBehaviour
{
    // Speed at which the platforms fall
    public float moveSpeed;
    // Bool to trigger the rise/fall of object
    public bool Move;

    private float initPos;
    public float TargetPos;

    // Use Awake for initializing
    void Awake()
    {
        initPos = transform.position.y;
    }

    // Update scene
    void Update()
    {
        if (Move)
        {
            if (this.name == "Wall")
            {
                //if (transform.position.y > TargetPos)
                //    transform.position -= Vector3.up * moveSpeed * Time.deltaTime;
                if (transform.position.y < TargetPos)
                    transform.position += Vector3.up * moveSpeed * Time.deltaTime;

            }

            if (this.name == "OrbRamp")
                if (transform.position.y < TargetPos)
                    transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
        else
        {
//            if (transform.position.y < initPos)
//                transform.position += Vector3.up * moveSpeed * Time.deltaTime;
//            // Raise the object
        }
    }
}