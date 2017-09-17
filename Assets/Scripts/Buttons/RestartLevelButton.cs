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
