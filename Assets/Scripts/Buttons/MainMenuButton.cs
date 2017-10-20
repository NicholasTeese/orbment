using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : BaseButton
{
    public override void OnCursorEnter()
    {
        base.OnCursorEnter();

        MainMenuManager.m_mainMenuManager.SelectedButtonIndex = m_iParentListIndex;
        MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = false;
        MainMenuManager.m_mainMenuManager.SelectedButton = MainMenuManager.m_mainMenuManager.ActivePanelButtons[m_iParentListIndex];
        MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = true;
    }

    public override void OnClick(string a_strParameter)
    {
        base.OnClick(a_strParameter);

        MainMenuManager.m_mainMenuManager.MainMenuAudioSource.PlayOneShot(m_menuClickAudioClip);

        switch (a_strParameter)
        {
            case "MainPanelPlay":
                {
                    MainMenuManager.m_mainMenuManager.FadeIn = false;
                    break;
                }

            case "MainPanelOptions":
                {
                    MainMenuManager.m_mainMenuManager.ActivePanelButtons = MainMenuManager.m_mainMenuManager.OptionsPanelButtons;
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = false;
                    MainMenuManager.m_mainMenuManager.SelectedButton = MainMenuManager.m_mainMenuManager.ActivePanelButtons[0];
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = true;
                    MainMenuManager.m_mainMenuManager.m_mainPanel.SetActive(false);
                    MainMenuManager.m_mainMenuManager.m_optionsPanel.SetActive(true);
                    MainMenuManager.m_mainMenuManager.ResetSelectedButtonIndex();
                    break;
                }

            case "MainPanelQuitToDesktop":
                {
                    MainMenuManager.m_mainMenuManager.ActivePanelButtons = MainMenuManager.m_mainMenuManager.QuitToDesktopPanelButtons;
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = false;
                    MainMenuManager.m_mainMenuManager.SelectedButton = MainMenuManager.m_mainMenuManager.ActivePanelButtons[0];
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = true;
                    MainMenuManager.m_mainMenuManager.m_mainPanel.SetActive(false);
                    MainMenuManager.m_mainMenuManager.m_quitToDesktopPanel.SetActive(true);
                    MainMenuManager.m_mainMenuManager.ResetSelectedButtonIndex();
                    break;
                }

            case "Show_Hide_Cursor":
                {
                    if (GameManager.m_gameManager.ShowCursor)
                    {
                        GameManager.m_gameManager.ShowCursor = false;
                        m_button.GetComponentInChildren<Text>().text = "Show Cursor";
                    }
                    else
                    {
                        GameManager.m_gameManager.ShowCursor = true;
                        m_button.GetComponentInChildren<Text>().text = "Hide Cursor";
                    }
                    break;
                }

            case "OptionsPanelBack":
                {
                    MainMenuManager.m_mainMenuManager.ActivePanelButtons = MainMenuManager.m_mainMenuManager.MainPanelButtons;
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = false;
                    MainMenuManager.m_mainMenuManager.SelectedButton = MainMenuManager.m_mainMenuManager.ActivePanelButtons[0];
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = true;
                    MainMenuManager.m_mainMenuManager.m_mainPanel.SetActive(true);
                    MainMenuManager.m_mainMenuManager.m_optionsPanel.SetActive(false);
                    MainMenuManager.m_mainMenuManager.ResetSelectedButtonIndex();
                    break;
                }

            case "QuitToDesktopPanelYes":
                {
                    GameManager.m_gameManager.QuitToDesktop();
                    break;
                }

            case "QuitToDesktopPanelNo":
                {
                    MainMenuManager.m_mainMenuManager.ActivePanelButtons = MainMenuManager.m_mainMenuManager.MainPanelButtons;
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = false;
                    MainMenuManager.m_mainMenuManager.SelectedButton = MainMenuManager.m_mainMenuManager.ActivePanelButtons[0];
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = true;
                    MainMenuManager.m_mainMenuManager.m_mainPanel.SetActive(true);
                    MainMenuManager.m_mainMenuManager.m_quitToDesktopPanel.SetActive(false);
                    MainMenuManager.m_mainMenuManager.ResetSelectedButtonIndex();
                    break;
                }

            default:
                {
                    Debug.Log("Case for " + a_strParameter + "could not be found.");
                    break;
                }
        }
    }
}
