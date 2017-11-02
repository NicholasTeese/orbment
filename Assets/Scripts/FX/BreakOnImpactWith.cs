using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOnImpactWith : MonoBehaviour
{
    private int wallHealth = 100;
    public int WallHealth { get { return wallHealth; } set { wallHealth = value; } }

    private AudioClip[] m_wallBreaks;

    private AudioSource m_audioSource;

    public string m_tag;
    public GameObject m_faceModel;
    public GameObject m_chunkModel;

    [HideInInspector]
    public Vector3 m_entranceVector;
    public bool m_isBroken = false;

    private void Awake()
    {
        m_wallBreaks = Resources.LoadAll<AudioClip>("Audio/Beta/Environment/Wall_Break");
    }

    // Use this for initialization
    void Start()
    {
        m_audioSource = IsoCam.m_playerCamera.GetComponent<AudioSource>();

        if (m_chunkModel != null)
        {
            m_chunkModel.SetActive(false);
        }

    }
    void Update()
    {
        if (wallHealth <= 0.0f && !m_isBroken)
        {
            m_audioSource.PlayOneShot(m_wallBreaks[Random.Range(0, m_wallBreaks.Length)]);
        }

        if (wallHealth <= 0)
        {
            m_faceModel.SetActive(false);

            m_chunkModel.SetActive(true);
            
            m_isBroken = true;
        }
        //else
        //{
        //    if (wallHealth <= 50)
        //    {
        //        Material[] breakWallMaterials = m_faceModel.GetComponent<Renderer>().materials;
        //        breakWallMaterials[0].mainTexture = null;
        //    }
        //}
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(m_tag) && !m_isBroken)
        {
            ///if player
            Player playerScript = other.GetComponent<Player>();

            if (playerScript != null && playerScript.m_dashing)
            {
                m_entranceVector = playerScript.m_dashDirection;
                wallHealth -= 55;
            }
        }
    }
}
