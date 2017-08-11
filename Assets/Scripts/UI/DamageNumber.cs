using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour
{
    public Transform parent;
    public Text m_text;
    public float m_textFadeSpeed = 2.0f;
    public float m_riseSpeed = 200.0f;
    public float m_lifeTime = 1.5f;
    private bool m_enabled;
    private float m_timer = 0.0f;
    private Vector2 m_translation;
    public Color m_color;
    // Use this for initialization
    void Start()
    {
        m_text = this.GetComponent<Text>();
    }

    private void OnEnable()
    {
        m_color = Color.white;
        m_enabled = true;
    }



    // Update is called once per frame
    void Update()
    {
        if(parent != null)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(parent.transform.position);
            this.transform.position = screenPos + m_translation;
           
        }
        else
        {
            //disable
            this.gameObject.SetActive(false);
            m_enabled = false;
        }
   
        if(m_enabled)
        {
            m_timer += Time.deltaTime;
            m_translation += Vector2.up * m_riseSpeed * Time.deltaTime;
            m_text.color = m_color;
            m_color.a -= m_textFadeSpeed*Time.deltaTime;

            
            if(m_timer >= m_lifeTime)
            {
                
                m_translation = Vector2.zero;
                m_timer = 0.0f;
                this.gameObject.SetActive(false);
                m_enabled = false;
            }
        }
    }
}
