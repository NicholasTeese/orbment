using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expand : MonoBehaviour
{
    public float m_scaleRate = 0.1f;
    //public float m_duration = 0.2f;
    //private float m_timer = 0.0f;
    //private float m_ratio = 0.0f;
    // Use this for initialization
    void Start()
    {

    }

    void OnEnable()
    {

        this.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
 

        this.transform.localScale += Vector3.one  * m_scaleRate;


    }
}
