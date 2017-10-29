using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSubMenu : MonoBehaviour
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
    }

    private void OnEscapePressed(GameObject a_parentSubMenu)
    {
        if (m_parentSubMenu == null)
        {
            return;
        }

        switch (a_parentSubMenu.name)
        {
            case "MainPanel":
                {
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = false;

                    if (gameObject.name != "Main_Options_Panel")
                    {
                        transform.gameObject.SetActive(false);
                    }
                    else
                    {
                        MainMenuManager.m_mainMenuManager.m_optionsPanel.SetActive(false);
                    }

                    MainMenuManager.m_mainMenuManager.m_mainPanel.SetActive(true);
                    MainMenuManager.m_mainMenuManager.ActivePanelButtons = MainMenuManager.m_mainMenuManager.MainPanelButtons;
                    MainMenuManager.m_mainMenuManager.SelectedButtonIndex = MainMenuManager.m_mainMenuManager.LastSelectedButtonIndex;
                    MainMenuManager.m_mainMenuManager.SelectedButton = MainMenuManager.m_mainMenuManager.ActivePanelButtons[MainMenuManager.m_mainMenuManager.SelectedButtonIndex];
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = true;
                    break;
                }

            case "Main_Options_Panel":
                {
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = false;
                    transform.gameObject.SetActive(false);
                    MainMenuManager.m_mainMenuManager.m_optionsPanel.SetActive(true);
                    MainMenuManager.m_mainMenuManager.OptionsMainPanel.SetActive(true);
                    MainMenuManager.m_mainMenuManager.ActivePanelButtons = MainMenuManager.m_mainMenuManager.OptionsMainPanelButtons;
                    MainMenuManager.m_mainMenuManager.SelectedButtonIndex = MainMenuManager.m_mainMenuManager.LastSelectedButtonIndex;
                    MainMenuManager.m_mainMenuManager.SelectedButton = MainMenuManager.m_mainMenuManager.ActivePanelButtons[MainMenuManager.m_mainMenuManager.SelectedButtonIndex];
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = true;
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
