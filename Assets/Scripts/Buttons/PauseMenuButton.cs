using UnityEngine;

public class PauseMenuButton : BaseButton
{
    public override void OnCursorEnter()
    {
        base.OnCursorEnter();

        PauseMenuManager.m_pauseMenuCanvasManager.SelectedButtonIndex = m_iParentListIndex;
        PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton.IsMousedOver = false;
        PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton = PauseMenuManager.m_pauseMenuCanvasManager.ActivePanelButtons[m_iParentListIndex];
        PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton.IsMousedOver = true;
    }

    public override void OnCursorExit()
    {
        base.OnCursorExit();
    }

    public override void OnClick(string a_strParameter)
    {
        base.OnClick(a_strParameter);

        switch (a_strParameter)
        {
            // Start Main Panel.
            case "MainPanelContinue":
                {
                    PauseMenuManager.m_pauseMenuCanvasManager.ActivePanelButtons = PauseMenuManager.m_pauseMenuCanvasManager.MainPanelButtons;
                    PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton.IsMousedOver = false;
                    PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton = PauseMenuManager.m_pauseMenuCanvasManager.ActivePanelButtons[0];
                    PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton.IsMousedOver = true;
                    PauseMenuManager.m_pauseMenuCanvasManager.m_mainPanel.SetActive(true);
                    PauseMenuManager.m_pauseMenuCanvasManager.m_optionsPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuCanvasManager.m_quitToMainMenuPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuCanvasManager.m_quitToDesktopPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuCanvasManager.ResetSelectedButtonIndex();
                    GameManager.m_gameManager.ContinueGame();
                    break;
                }

            case "MainPanelOptions":
                {
                    PauseMenuManager.m_pauseMenuCanvasManager.ActivePanelButtons = PauseMenuManager.m_pauseMenuCanvasManager.OptionsPanelbuttons;
                    PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton.IsMousedOver = false;
                    PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton = PauseMenuManager.m_pauseMenuCanvasManager.ActivePanelButtons[0];
                    PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton.IsMousedOver = true;
                    PauseMenuManager.m_pauseMenuCanvasManager.m_mainPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuCanvasManager.m_optionsPanel.SetActive(true);
                    PauseMenuManager.m_pauseMenuCanvasManager.m_quitToMainMenuPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuCanvasManager.m_quitToDesktopPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuCanvasManager.ResetSelectedButtonIndex();
                    break;
                }

            case "MainPanelQuitToMainMenu":
                {
                    PauseMenuManager.m_pauseMenuCanvasManager.ActivePanelButtons = PauseMenuManager.m_pauseMenuCanvasManager.QuitToMainMenuPanelButtons;
                    PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton.IsMousedOver = false;
                    PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton = PauseMenuManager.m_pauseMenuCanvasManager.ActivePanelButtons[0];
                    PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton.IsMousedOver = true;
                    PauseMenuManager.m_pauseMenuCanvasManager.m_mainPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuCanvasManager.m_optionsPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuCanvasManager.m_quitToMainMenuPanel.SetActive(true);
                    PauseMenuManager.m_pauseMenuCanvasManager.m_quitToDesktopPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuCanvasManager.ResetSelectedButtonIndex();
                    break;
                }

            case "MainPanelQuitToDesktop":
                {
                    PauseMenuManager.m_pauseMenuCanvasManager.ActivePanelButtons = PauseMenuManager.m_pauseMenuCanvasManager.QuitToDesktopPanelButtons;
                    PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton.IsMousedOver = false;
                    PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton = PauseMenuManager.m_pauseMenuCanvasManager.ActivePanelButtons[0];
                    PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton.IsMousedOver = true;
                    PauseMenuManager.m_pauseMenuCanvasManager.m_mainPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuCanvasManager.m_optionsPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuCanvasManager.m_quitToMainMenuPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuCanvasManager.m_quitToDesktopPanel.SetActive(true);
                    PauseMenuManager.m_pauseMenuCanvasManager.ResetSelectedButtonIndex();
                    break;
                }
            // End Main Panel.

            // Options Panel Start.
            case "OptionsPanelBack":
                {
                    PauseMenuManager.m_pauseMenuCanvasManager.ActivePanelButtons = PauseMenuManager.m_pauseMenuCanvasManager.MainPanelButtons;
                    PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton.IsMousedOver = false;
                    PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton = PauseMenuManager.m_pauseMenuCanvasManager.ActivePanelButtons[0];
                    PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton.IsMousedOver = true;
                    PauseMenuManager.m_pauseMenuCanvasManager.m_mainPanel.SetActive(true);
                    PauseMenuManager.m_pauseMenuCanvasManager.m_optionsPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuCanvasManager.m_quitToMainMenuPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuCanvasManager.m_quitToDesktopPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuCanvasManager.ResetSelectedButtonIndex();
                    break;
                }
            // Options Panel End.

            // Quit To Main Menu Panel Start.
            case "QuitToMainMenuYes":
                {
                    GameManager.m_gameManager.QuitToMain();
                    break;
                }

            case "QuitToMainMenuNo":
                {
                    PauseMenuManager.m_pauseMenuCanvasManager.ActivePanelButtons = PauseMenuManager.m_pauseMenuCanvasManager.MainPanelButtons;
                    PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton.IsMousedOver = false;
                    PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton = PauseMenuManager.m_pauseMenuCanvasManager.ActivePanelButtons[0];
                    PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton.IsMousedOver = true;
                    PauseMenuManager.m_pauseMenuCanvasManager.m_mainPanel.SetActive(true);
                    PauseMenuManager.m_pauseMenuCanvasManager.m_optionsPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuCanvasManager.m_quitToMainMenuPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuCanvasManager.m_quitToDesktopPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuCanvasManager.ResetSelectedButtonIndex();
                    break;
                }
            // Quit to Main Menu Panel End.

            // Quit To Desktop Panel Start.
            case "QuitToDesktopYes":
                {
                    GameManager.m_gameManager.QuitToDesktop();
                    break;
                }

            case "QuitToDesktopNo":
                {
                    PauseMenuManager.m_pauseMenuCanvasManager.ActivePanelButtons = PauseMenuManager.m_pauseMenuCanvasManager.MainPanelButtons;
                    PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton.IsMousedOver = false;
                    PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton = PauseMenuManager.m_pauseMenuCanvasManager.ActivePanelButtons[0];
                    PauseMenuManager.m_pauseMenuCanvasManager.SelectedButton.IsMousedOver = true;
                    PauseMenuManager.m_pauseMenuCanvasManager.m_mainPanel.SetActive(true);
                    PauseMenuManager.m_pauseMenuCanvasManager.m_optionsPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuCanvasManager.m_quitToMainMenuPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuCanvasManager.m_quitToDesktopPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuCanvasManager.ResetSelectedButtonIndex();
                    break;
                }
            // Quit to Desktop Panel End.

            default:
                {
                    Debug.Log("Case for " + a_strParameter + "could not be found.");
                    break;
                }
        }
    }
}
