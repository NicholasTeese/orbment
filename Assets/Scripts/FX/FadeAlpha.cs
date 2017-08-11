using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAlpha : MonoBehaviour
{
    private Renderer m_renderer;
    public float m_duration = 0.5f;
    private float m_timer = 0.0f;
    public float m_ratio = 0.0f;
    public float m_initialAlpha = 0.2f;
    private Material m_material;
    // Use this for initialization
    void Start()
    {
        m_renderer = this.GetComponent<Renderer>();
        if (m_renderer != null)
        {
            m_material = m_renderer.material;
        }
    }

    void OnEnable()
    {
        if(m_material != null)
        {
            m_material.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, m_initialAlpha));
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_ratio = m_timer / m_duration;

        if (m_material != null)
        {
            m_material.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, m_initialAlpha - m_ratio ));
        }
        
        m_timer += Time.deltaTime;

        if(m_timer >= m_duration)
        {
            m_timer = 0.0f;
        }
    }
}
