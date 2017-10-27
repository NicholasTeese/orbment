using UnityEngine;

public class BaseMenuItem : MonoBehaviour
{
    public enum Type
    {
        BUTTON,
        SLIDER
    }

    protected int m_iParentListIndex = 0;

    protected float m_fGrowShrinkSpeed = 0.1f;
    protected float m_fGrowMultiplier = 1.5f;
    protected float m_fShrinkMultiplier = 1.0f;

    protected bool m_bIsMousedOver = false;

    protected Type m_eType;

    protected BaseMenuItem m_parentMenuItem;

    public string m_strOnClickParameter;

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
            Debug.Log(gameObject.name + " menu item cannot be clicked because 'm_bIsMouseOver' is " + m_bIsMousedOver + '.');
            return;
        }
    }

    public virtual void OnClick(string a_strParameter)
    {
        if (!m_bIsMousedOver)
        {
            Debug.Log(gameObject.name + " menu item cannot be clicked because 'm_bIsMouseOver' is " + m_bIsMousedOver + '.');
            return;
        }
    }
}