using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//-------------------------------------------------------------------------------------------------------------------------------------------------------------

// Creator: Vince & John
// Additions: Taliesin Millhouse & Nicholas Teese
// Description: Isometric camera for following the player and handling screen shaking as well as red screen flash for indicating damage

//-------------------------------------------------------------------------------------------------------------------------------------------------------------


public class IsoCam : MonoBehaviour
{
    public float m_camMoveSpeed = 10;
    public float m_camRotSpeed = 10;
    public Material m_flashRed;


    public GameObject m_target;
    private Vector3 offset;

    private bool m_shake = false;
    private float m_shakeTimer = 0.0f;
    private float m_shakeDuration = 0.0f;
    public float m_shakeAmount = 0.0f;


    private bool m_flashingRed = false;
    private float m_flashTimer = 0.0f;
    private float m_flashDuration = 0.0f;
    private float m_intensity = 0.0f;

    GameObject parent = null;

    // Use this for initialization
    void Start()
    {
        offset = this.transform.position - m_target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_target != null)
        {
           this.transform.position = Vector3.MoveTowards(this.transform.position, m_target.transform.position + offset, m_camMoveSpeed);
           this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(m_target.transform.position - this.transform.position), m_camRotSpeed);
        }

        if(m_shake)
        {
            m_shakeTimer += Time.deltaTime;

            Shake(m_shakeAmount, m_shakeDuration);

            if(m_shakeTimer >= m_shakeDuration)
            {
                m_shake = false;
                m_shakeTimer = 0.0f;
            }
        }

        if(m_flashingRed)
        {
            m_flashTimer += Time.deltaTime;
            FlashRed(m_flashDuration);

            if(m_flashDuration != 0.0f)
            {
                m_intensity = 1.0f - (m_flashTimer / m_flashDuration);
            }
                        
            if(m_flashTimer >= m_flashDuration)
            {
                m_intensity = 0.0f;
                m_flashTimer = 0.0f;
                m_flashingRed = false;
            }           
        }
        else
        {
            if (m_flashRed != null)
            {
                m_flashRed.SetFloat("_Intensity", 0.0f);
            }
        }

        //TODO: Waiting on partially transparent textures.
        //Debug.DrawRay(transform.position, (Player.m_Player.transform.position - transform.position), Color.red);
        //Ray ray = GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        //int iLayerMask = LayerMask.GetMask("Object");
        //RaycastHit rayCastHit;
        //if (Physics.Raycast(ray, out rayCastHit, 1000, iLayerMask))
        //{
        //    Debug.Log(rayCastHit.transform.name);

        //    if (rayCastHit.transform.parent != null && rayCastHit.transform.parent.gameObject.layer == 16)
        //    {
        //        parent = rayCastHit.transform.parent.gameObject;
        //        Color color = parent.transform.GetComponent<MeshRenderer>().material.color;
        //        parent.transform.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 0.3f);

        //        //if (parent.transform.childCount != 0)
        //        //{
        //        //    foreach (GameObject leaf in parent.transform)
        //        //    {
        //        //        Color colorr = parent.transform.GetComponent<MeshRenderer>().material.color;
        //        //        leaf.transform.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 0.3f);
        //        //    }
        //        //}
        //    }
        //    else
        //    {
        //        parent = rayCastHit.transform.gameObject;
        //        Color color = parent.transform.GetComponent<MeshRenderer>().material.color;
        //        parent.transform.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 0.3f);

        //        //if (parent.transform.childCount != 0)
        //        //{
        //        //    foreach (GameObject leaf in parent.transform)
        //        //    {
        //        //        Color colorr = parent.transform.GetComponent<MeshRenderer>().material.color;
        //        //        leaf.transform.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 0.3f);
        //        //    }
        //        //}
        //    }
        //}
        //else if (parent != null)
        //{
        //    Debug.Log("Nothing");
        //    Color color = parent.transform.GetComponent<MeshRenderer>().material.color;
        //    parent.transform.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 1.0f);
        //    parent = null;

        //    //if (parent.transform.childCount != 0)
        //    //{
        //    //    foreach (GameObject leaf in parent.transform)
        //    //    {
        //    //        Color colorr = parent.transform.GetComponent<MeshRenderer>().material.color;
        //    //        leaf.transform.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 0.3f);
        //    //    }
        //    //}
        //}
    }

    public void Shake(float a_shakeAmount, float a_shakeDuration)
    {
        m_shakeAmount = a_shakeAmount;
        m_shakeDuration = a_shakeDuration;
       
        m_shake = true;
        this.transform.position += Random.onUnitSphere * a_shakeAmount * Time.deltaTime;
    }

    public void FlashRed(float m_duration)
    {
        m_flashDuration = m_duration;

        m_flashingRed = true;

        if(m_flashRed != null)
        {
            m_flashRed.SetFloat("_Intensity", m_intensity);
        }
    }
}