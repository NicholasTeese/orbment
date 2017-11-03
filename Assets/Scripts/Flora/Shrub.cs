using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrub : MonoBehaviour
{
    private Color m_randomColor;

    public Renderer m_renderer;

    private void Awake()
    {
        if (m_renderer == null)
        {
            m_renderer = GetComponent<Renderer>();
        }

        //m_randomColor = m_renderer.material.color;
        m_randomColor = Color.red;
        m_renderer.material.color = m_randomColor;
    }
}
