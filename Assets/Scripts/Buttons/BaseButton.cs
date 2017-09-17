using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseButton : MonoBehaviour
{
    protected float m_fGrowShrinkSpeed = 0.1f;
    protected float m_fGrowMultiplier = 1.5f;
    protected float m_fShrinkMultiplier = 1.0f;

    protected bool m_bIsMousedOver = false;
    public bool IsMousedOver { get { return m_bIsMousedOver; } set { m_bIsMousedOver = value; } }

    protected Button m_button;

    protected virtual void Awake()
    {
        m_button = GetComponent<Button>();
    }

    protected virtual void Update()
    {
        if (m_bIsMousedOver)
        {
            Grow();
        }
        else
        {
            Shrink();
        }
    }

    public virtual void OnCursorEnter()
    {
        m_bIsMousedOver = true;
    }

    public virtual void OnCursorExit()
    {
        m_bIsMousedOver = false;
    }

    public virtual void OnClick()
    {
        if (!m_bIsMousedOver)
        {
            Debug.Log(gameObject.name + " button cannot be clicked because 'm_bIsMouseOver' is " + m_bIsMousedOver + '.');
            return;
        }
    }

    public virtual void OnClick(string a_strParameter)
    {

    }

    protected virtual void Grow()
    {
        if (transform.localScale.x < m_fGrowMultiplier)
        {
            transform.localScale += new Vector3(m_fGrowShrinkSpeed, m_fGrowShrinkSpeed, 0.0f);
        }
    }

    protected virtual void Shrink()
    {
        if (transform.localScale.x > m_fShrinkMultiplier)
        {
            transform.localScale -= new Vector3(m_fGrowShrinkSpeed, m_fGrowShrinkSpeed, 0.0f);
        }
    }
}
