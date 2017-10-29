using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuButton : BaseButton
{
    public override void OnCursorEnter()
    {
        base.OnCursorEnter();

        DeathMenuManager.m_deathMenuManager.SelectedButtonIndex = m_iParentListIndex;
        DeathMenuManager.m_deathMenuManager.SelectedButton.IsMousedOver = false;
        DeathMenuManager.m_deathMenuManager.SelectedButton = DeathMenuManager.m_deathMenuManager.ActivePanelButtons[m_iParentListIndex];
        DeathMenuManager.m_deathMenuManager.SelectedButton.IsMousedOver = true;
    }

    public override void OnClick(string a_strParameter)
    {
        base.OnClick(a_strParameter);

        DeathMenuManager.m_deathMenuManager.DeathMenuAudioSource.PlayOneShot(m_menuClickAudioClip);
        DeathMenuManager.m_deathMenuManager.LastSelectedButtonIndex = DeathMenuManager.m_deathMenuManager.SelectedButtonIndex;

        switch (a_strParameter)
        {
            // Main panel start.
            case "MainPanelTryAgain":
                {
                    if (SceneManager.GetActiveScene().name == LevelManager.m_strTutorialSceneName)
                    {
                        LevelManager.m_levelManager.DestroyAllDontDestroyOnLoad();
                        SceneManager.LoadScene(LevelManager.m_strTutorialSceneName);
                    }
                    else
                    {
                        LevelManager.m_levelManager.DestroyAllDontDestroyOnLoad();
                        SceneManager.LoadScene(LevelManager.m_strLevelOneSceneName);
                    }
                    break;
                }

            case "MainPanelQuitToMainMenu":
                {
                    DeathMenuManager.m_deathMenuManager.ActivePanelButtons = DeathMenuManager.m_deathMenuManager.QuitToMainMenuPanelButtons;
                    DeathMenuManager.m_deathMenuManager.SelectedButton.IsMousedOver = false;
                    DeathMenuManager.m_deathMenuManager.SelectedButton = DeathMenuManager.m_deathMenuManager.ActivePanelButtons[0];
                    DeathMenuManager.m_deathMenuManager.SelectedButton.IsMousedOver = true;
                    DeathMenuManager.m_deathMenuManager.MainPanel.SetActive(false);
                    DeathMenuManager.m_deathMenuManager.QuitToMainMenuPanel.SetActive(true);
                    DeathMenuManager.m_deathMenuManager.QuitToDesktopPanel.SetActive(false);
                    DeathMenuManager.m_deathMenuManager.ResetSelectedButtonIndex();
                    break;
                }

            case "MainPanelQuitToDesktop":
                {
                    DeathMenuManager.m_deathMenuManager.ActivePanelButtons = DeathMenuManager.m_deathMenuManager.QuitToDesktopPanelButtons;
                    DeathMenuManager.m_deathMenuManager.SelectedButton.IsMousedOver = false;
                    DeathMenuManager.m_deathMenuManager.SelectedButton = DeathMenuManager.m_deathMenuManager.ActivePanelButtons[0];
                    DeathMenuManager.m_deathMenuManager.SelectedButton.IsMousedOver = true;
                    DeathMenuManager.m_deathMenuManager.MainPanel.SetActive(false);
                    DeathMenuManager.m_deathMenuManager.QuitToMainMenuPanel.SetActive(false);
                    DeathMenuManager.m_deathMenuManager.QuitToDesktopPanel.SetActive(true);
                    DeathMenuManager.m_deathMenuManager.ResetSelectedButtonIndex();
                    break;
                }
            // Main panel end.
            
            // Quit to main menu panel start.
            case "QuitToMainMenuYes":
                {
                    GameManager.m_gameManager.LoadMainMenu();
                    break;
                }

            case "QuitToMainMenuNo":
                {
                    DeathMenuManager.m_deathMenuManager.ActivePanelButtons = DeathMenuManager.m_deathMenuManager.MainPanelButtons;
                    DeathMenuManager.m_deathMenuManager.SelectedButton.IsMousedOver = false;
                    DeathMenuManager.m_deathMenuManager.SelectedButton = DeathMenuManager.m_deathMenuManager.ActivePanelButtons[0];
                    DeathMenuManager.m_deathMenuManager.SelectedButton.IsMousedOver = true;
                    DeathMenuManager.m_deathMenuManager.MainPanel.SetActive(true);
                    DeathMenuManager.m_deathMenuManager.QuitToMainMenuPanel.SetActive(false);
                    DeathMenuManager.m_deathMenuManager.QuitToDesktopPanel.SetActive(false);
                    DeathMenuManager.m_deathMenuManager.ResetSelectedButtonIndex();
                    break;
                }
            // Quit to main menu panel end.

            // Quit to desktop panel start.
            case "QuitToDesktopYes":
                {
                    GameManager.m_gameManager.QuitToDesktop();
                    break;
                }

            case "QuitToDesktopNo":
                {
                    DeathMenuManager.m_deathMenuManager.ActivePanelButtons = DeathMenuManager.m_deathMenuManager.MainPanelButtons;
                    DeathMenuManager.m_deathMenuManager.SelectedButton.IsMousedOver = false;
                    DeathMenuManager.m_deathMenuManager.SelectedButton = DeathMenuManager.m_deathMenuManager.ActivePanelButtons[0];
                    DeathMenuManager.m_deathMenuManager.SelectedButton.IsMousedOver = true;
                    DeathMenuManager.m_deathMenuManager.MainPanel.SetActive(true);
                    DeathMenuManager.m_deathMenuManager.QuitToMainMenuPanel.SetActive(false);
                    DeathMenuManager.m_deathMenuManager.QuitToDesktopPanel.SetActive(false);
                    DeathMenuManager.m_deathMenuManager.ResetSelectedButtonIndex();
                    break;
                }
            // Quit to desktop panel end.

            default:
                {
                    Debug.Log("Case for " + a_strParameter + "could not be found.");
                    break;
                }
        }
    }
}
