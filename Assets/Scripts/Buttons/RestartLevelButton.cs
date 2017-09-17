using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class RestartLevelButton : BaseButton
{
    public override void OnCursorEnter()
    {
        base.OnCursorEnter();

        DeathMenuManager.m_deathMenuManager.SelectedButton.IsMousedOver = false;
        DeathMenuManager.m_deathMenuManager.SelectedButton = this;
        DeathMenuManager.m_deathMenuManager.SelectedButton.IsMousedOver = true;
    }

    public override void OnCursorExit()
    {
        base.OnCursorExit();
    }

    public override void OnClick()
    {
        if (!m_bIsMousedOver)
        {
            Debug.Log(gameObject.name + " button cannot be clicked because 'm_bIsMouseOver' is " + m_bIsMousedOver + '.');
            return;
        }

        base.OnClick();
        GameManager.m_gameManager.RestartLevel();
    }

    protected override void Update()
    {
        base.Update();

        if (InputManager.AButton())
        {
            OnClick();
        }
    }
}
