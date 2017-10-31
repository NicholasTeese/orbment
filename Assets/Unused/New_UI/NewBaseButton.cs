using UnityEngine;
using UnityEngine.UI;

public class NewBaseButton : BaseMenuItem
{
    protected Button m_button;

    protected virtual void Awake()
    {
        m_eType = Type.BUTTON;

        m_button = GetComponent<Button>();
    }

    protected virtual void Update()
    {
        if (m_bIsMousedOver)
        {
            Select();
        }
        else
        {
            Deselect();
        }
    }

    public override void OnCursorEnter()
    {
        base.OnCursorEnter();
    }

    public override void OnCursorExit()
    {
        base.OnCursorExit();
    }

    public override void OnClick()
    {
        base.OnClick();
    }

    public override void OnClick(string a_strParameter)
    {
        base.OnClick(a_strParameter);
    }

    protected virtual void Select()
    {
        if (transform.localScale.x < m_fGrowMultiplier)
        {
            transform.localScale += new Vector3(m_fGrowShrinkSpeed, m_fGrowShrinkSpeed, 0.0f);
        }
    }

    protected virtual void Deselect()
    {
        if (transform.localScale.x > m_fShrinkMultiplier)
        {
            transform.localScale -= new Vector3(m_fGrowShrinkSpeed, m_fGrowShrinkSpeed, 0.0f);
        }
    }
}