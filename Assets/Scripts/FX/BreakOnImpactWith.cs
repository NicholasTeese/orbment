using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOnImpactWith : MonoBehaviour
{
	public AudioSource audiosrc;
    public string m_tag;
    public GameObject m_faceModel;
    public GameObject m_chunkModel;

    [HideInInspector]
    public Vector3 m_entranceVector;
    public bool m_isBroken = false;
    
    // Use this for initialization
    void Start()
    {
        if (m_chunkModel != null)
        {
            m_chunkModel.SetActive(false);
        }
       
    }


    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag(m_tag) && !m_isBroken)
        {



            ///if player
            Player playerScript = other.GetComponent<Player>();

            if(playerScript != null && playerScript.m_dashing)
            {
                m_faceModel.SetActive(false);

                m_chunkModel.SetActive(true);

                m_entranceVector = playerScript.m_dashDirection;
                m_isBroken = true;
				audiosrc.pitch = 1 + Random.Range (-0.4f, 0.3f);
            }
        }

       
    }


}
