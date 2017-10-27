using UnityEngine;
using UnityEngine.UI;

public class NewBaseSlider : BaseMenuItem
{
    protected Slider m_slider;

    protected virtual void Awake()
    {
        m_eType = Type.SLIDER;

        m_slider = GetComponent<Slider>();
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

    protected virtual void Select()
    {
        ColorBlock colorBlock = m_slider.colors;
        colorBlock.normalColor = Color.yellow;
        m_slider.colors = colorBlock;
    }

    protected virtual void Deselect()
    {
        ColorBlock colorBlock = m_slider.colors;
        colorBlock.normalColor = Color.white;
        m_slider.colors = colorBlock;
    }
}
