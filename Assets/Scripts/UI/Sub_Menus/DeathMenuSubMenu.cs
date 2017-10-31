using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenuSubMenu : MonoBehaviour
{
    public GameObject m_parentSubMenu;

    private void Awake()
    {
        if (m_parentSubMenu == null && gameObject.name != "MainPanel")
        {
            Debug.Log("m_parentSubMenu GameObject reference on DeathMenuSubMenu not set.");
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
                    PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = false;
                    transform.gameObject.SetActive(false);
                    DeathMenuManager.m_deathMenuManager.MainPanel.SetActive(true);
                    DeathMenuManager.m_deathMenuManager.ActivePanelButtons = DeathMenuManager.m_deathMenuManager.MainPanelButtons;
                    DeathMenuManager.m_deathMenuManager.SelectedButtonIndex = DeathMenuManager.m_deathMenuManager.LastSelectedButtonIndex;
                    DeathMenuManager.m_deathMenuManager.SelectedButton = DeathMenuManager.m_deathMenuManager.ActivePanelButtons[DeathMenuManager.m_deathMenuManager.SelectedButtonIndex];
                    DeathMenuManager.m_deathMenuManager.SelectedButton.IsMousedOver = true;
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
