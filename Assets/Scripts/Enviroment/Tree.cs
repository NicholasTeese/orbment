using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private List<Texture> m_leaves = new List<Texture>();

    private Texture m_mainTexture = null;

    public Texture m_trunkTextureOpaque;
    public Texture m_trunkTextureTransparent;
    public Texture m_leavesTextureOpaque;
    public Texture m_leavesTextureTrasparent;

    private void Awake()
    {
        m_mainTexture = GetComponent<Renderer>().material.mainTexture;

        foreach (Transform child in transform)
        {
            if (child.CompareTag("Leaves"))
            {
                //m_leaves.Add(child.GetComponent<Renderer>().material);
            }
        }

        SetTransparent();
    }

    public void SetTransparent()
    {
        //m_mainTexture = m_trunkTextureTransparent.mainTexture;

        if (m_leaves.Count != 0)
        {
            for (int iCount = 0; iCount < m_leaves.Count; ++iCount)
            {
                m_leaves[iCount] = m_leavesTextureTrasparent;
            }
        }
    }

    public void SetOpaque()
    {
        //m_mainTexture = m_trunkTextureOpaque.mainTexture;

        if (m_leaves.Count != 0)
        {
            for (int iCount = 0; iCount < m_leaves.Count; ++iCount)
            {
                m_leaves[iCount] = m_leavesTextureTrasparent;
            }
        }
    }

}
