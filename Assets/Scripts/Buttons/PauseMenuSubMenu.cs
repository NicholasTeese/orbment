using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuSubMenu : MonoBehaviour
{
    public GameObject m_parentSubMenu;

    private void Awake()
    {
        if (m_parentSubMenu == null && gameObject.name != "MainPanel")
        {
            Debug.Log("m_parentSubMenu GameObject reference on PauseMenuSubMenu not set.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || InputManager.BackButton())
        {
            OnEscapePressed(m_parentSubMenu);
        }

        if (InputManager.StartButton())
        {
            PauseMenuManager.m_pauseMenuManager.ActivePanelButtons = PauseMenuManager.m_pauseMenuManager.MainPanelButtons;
            PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = false;
            PauseMenuManager.m_pauseMenuManager.SelectedButton = PauseMenuManager.m_pauseMenuManager.ActivePanelButtons[0];
            PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = true;
            PauseMenuManager.m_pauseMenuManager.m_mainPanel.SetActive(true);
            PauseMenuManager.m_pauseMenuManager.m_optionsPanel.SetActive(false);
            PauseMenuManager.m_pauseMenuManager.m_quitToMainMenuPanel.SetActive(false);
            PauseMenuManager.m_pauseMenuManager.m_quitToDesktopPanel.SetActive(false);
            PauseMenuManager.m_pauseMenuManager.OptionsMainPanel.SetActive(true);
            PauseMenuManager.m_pauseMenuManager.OptionsAudioPanel.SetActive(false);
            PauseMenuManager.m_pauseMenuManager.OptionsGeneralPanel.SetActive(false);
            GameManager.m_gameManager.Continue();
        }
    }

    private void OnEscapePressed(GameObject a_parentSubMenu)
    {
        if (m_parentSubMenu == null)
        {
            PauseMenuManager.m_pauseMenuManager.ActivePanelButtons = PauseMenuManager.m_pauseMenuManager.MainPanelButtons;
            PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = false;
            PauseMenuManager.m_pauseMenuManager.SelectedButton = PauseMenuManager.m_pauseMenuManager.ActivePanelButtons[0];
            PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = true;
            PauseMenuManager.m_pauseMenuManager.m_mainPanel.SetActive(true);
            PauseMenuManager.m_pauseMenuManager.m_optionsPanel.SetActive(false);
            PauseMenuManager.m_pauseMenuManager.m_quitToMainMenuPanel.SetActive(false);
            PauseMenuManager.m_pauseMenuManager.m_quitToDesktopPanel.SetActive(false);
            PauseMenuManager.m_pauseMenuManager.ResetSelectedButtonIndex();
            GameManager.m_gameManager.Continue();
            return;
        }

        switch (a_parentSubMenu.name)
        {
            case "MainPanel":
                {
                    PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = false;

                    if (gameObject.name != "Main_Options_Panel")
                    {
                        transform.gameObject.SetActive(false);
                    }
                    else
                    {
                        PauseMenuManager.m_pauseMenuManager.m_optionsPanel.SetActive(false);
                    }

                    PauseMenuManager.m_pauseMenuManager.m_mainPanel.SetActive(true);
                    PauseMenuManager.m_pauseMenuManager.ActivePanelButtons = PauseMenuManager.m_pauseMenuManager.MainPanelButtons;
                    PauseMenuManager.m_pauseMenuManager.SelectedButtonIndex = PauseMenuManager.m_pauseMenuManager.LastSelectedButtonIndex;
                    PauseMenuManager.m_pauseMenuManager.SelectedButton = PauseMenuManager.m_pauseMenuManager.ActivePanelButtons[PauseMenuManager.m_pauseMenuManager.SelectedButtonIndex];
                    PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = true;
                    break;
                }

            case "Main_Options_Panel":
                {
                    PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = false;
                    transform.gameObject.SetActive(false);
                    PauseMenuManager.m_pauseMenuManager.m_optionsPanel.SetActive(true);
                    PauseMenuManager.m_pauseMenuManager.OptionsMainPanel.SetActive(true);
                    PauseMenuManager.m_pauseMenuManager.ActivePanelButtons = PauseMenuManager.m_pauseMenuManager.OptionsMainPanelButtons;
                    PauseMenuManager.m_pauseMenuManager.SelectedButtonIndex = PauseMenuManager.m_pauseMenuManager.LastSelectedButtonIndex;
                    PauseMenuManager.m_pauseMenuManager.SelectedButton = PauseMenuManager.m_pauseMenuManager.ActivePanelButtons[PauseMenuManager.m_pauseMenuManager.SelectedButtonIndex];
                    PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = true;
                    break;
                }

            default:
                {
                    Debug.Log("Parent sub menu name '" + a_parentSubMenu.name + "' not recognised.");
                    break;
                }
        }
    }
}
