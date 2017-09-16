using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftManager : MonoBehaviour
{
    // Speed at which the platforms fall
    public float moveSpeed;
    // Bool to trigger the rise/fall of object
    public bool Sink;

    private float initPos;
    private float lowerPos;
    public float TargetPos;

    // Use Awake for initializing
    void Awake()
    {
        initPos = transform.position.y;
        lowerPos = transform.position.y - 10.0f;
    }

    // Update scene
    void Update()
    {
        if (Sink)
        {
            if (transform.position.y > TargetPos)
                transform.position -= Vector3.up * moveSpeed * Time.deltaTime;
            // Lower the object
        }
        else
        {
            if (transform.position.y < initPos)
                transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            // Raise the object
        }
    }

    void OnTriggerEnter()
    {
        Sink = false;
        
    }
}