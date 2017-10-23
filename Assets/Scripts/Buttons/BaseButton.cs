using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseButton : MonoBehaviour
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

    protected AudioClip m_menuClickAudioClip;

    protected Button m_button;

    protected Slider m_slider;

    public string m_strOnClickParameter;

    public int ParentListIndex { get { return m_iParentListIndex; } set { m_iParentListIndex = value; } }

    public bool IsMousedOver { get { return m_bIsMousedOver; } set { m_bIsMousedOver = value; } }

    public Slider VolumeSlider { get { return m_slider; } }

    protected virtual void Awake()
    {
        m_menuClickAudioClip = Resources.Load("Audio/Beta/UI/Menu_Click") as AudioClip;

        if (GetComponent<Button>() != null)
        {
            m_eType = Type.BUTTON;
            m_button = GetComponent<Button>();
        }
        else if (GetComponent<Slider>() != null)
        {
            m_eType = Type.SLIDER;
            m_slider = GetComponent<Slider>();
        }
    }

    protected void OnEnable()
    {
        switch (m_strOnClickParameter)
        {
            case "Master_Volume":
                {
                    m_slider.value = AudioManager.m_audioManager.MasterVolume;
                    break;
                }

            case "Music_Volume":
                {
                    m_slider.value = AudioManager.m_audioManager.MusicVolume;
                    break;
                }

            case "Bullet_Volume":
                {
                    m_slider.value = AudioManager.m_audioManager.BulletVolume;
                    break;
                }

            case "Effects_Volume":
                {
                    m_slider.value = AudioManager.m_audioManager.EffectsVolume;
                    break;
                }

            case "Menu_Volume":
                {
                    m_slider.value = AudioManager.m_audioManager.MenuVolume;
                    break;
                }
        }
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
        if (!m_bIsMousedOver)
        {
            Debug.Log(gameObject.name + " button cannot be clicked because 'm_bIsMouseOver' is " + m_bIsMousedOver + '.');
            return;
        }
    }

    public virtual void OnValueChanged(string a_strParameter)
    {
        if (!m_bIsMousedOver)
        {
            Debug.Log(gameObject.name + " slider value cannot be changed because 'm_bIsMouseOver' is " + m_bIsMousedOver + '.');
            return;
        }
    }

    protected virtual void Select()
    {
        if (m_eType == Type.BUTTON)
        {
            if (transform.localScale.x < m_fGrowMultiplier)
            {
                transform.localScale += new Vector3(m_fGrowShrinkSpeed, m_fGrowShrinkSpeed, 0.0f);
            }
        }
        else
        {
            ColorBlock colorBlock = m_slider.colors;
            colorBlock.normalColor = Color.yellow;
            m_slider.colors = colorBlock;
        }
    }

    protected virtual void Deselect()
    {
        if (m_eType == Type.BUTTON)
        {
            if (transform.localScale.x > m_fShrinkMultiplier)
            {
                transform.localScale -= new Vector3(m_fGrowShrinkSpeed, m_fGrowShrinkSpeed, 0.0f);
            }
        }
        else
        {
            ColorBlock colorBlock = m_slider.colors;
            colorBlock.normalColor = Color.white;
            m_slider.colors = colorBlock;
        }
    }
}
