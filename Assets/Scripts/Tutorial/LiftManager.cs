using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftManager : MonoBehaviour
{
    // Speed at which the platforms fall
    public float moveSpeed;
    // Bool to trigger the rise/fall of object
    public bool Move;
    public GameObject wall;
    public GameObject door;

    private float initPos;
    public float TargetPos;

    // Use Awake for initializing
    void Awake()
    {
        initPos = wall.transform.position.y;
    }

    // Update scene
    void Update()
    {
        if (Move)
        {
            if (wall != null)
            {            
                //if (transform.position.y > TargetPos)
                //    transform.position -= Vector3.up * moveSpeed * Time.deltaTime;
                //if (transform.position.y < TargetPos)
                //{
                //    transform.position += Vector3.up * moveSpeed * Time.deltaTime;
                //}
                if (wall.transform.position.y < TargetPos)
                {
                    wall.transform.position += Vector3.up * moveSpeed * Time.deltaTime;
                }
                if (door != null)
                {
                    door.SetActive(false);
                }
            }
            else
            {
                Debug.Log("Wall not found.");
            }

            if (this.name == "OrbRamp")
            {
                if (transform.position.y < TargetPos)
                {
                    transform.position += Vector3.up * moveSpeed * Time.deltaTime;
                }
            }
        }
        else
        {
//            if (transform.position.y < initPos)
//                transform.position += Vector3.up * moveSpeed * Time.deltaTime;
//            // Raise the object
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Move = true;
        }
    }
}