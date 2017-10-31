using UnityEngine;
using UnityEngine.UI;

public sealed class NewPauseMenuButton : NewBaseButton
{
    public override sealed void OnCursorEnter()
    {
        base.OnCursorEnter();
        SelectNewButton(m_iParentListIndex);
    }

    public override sealed void OnClick(string a_strParameter)
    {
        base.OnClick(a_strParameter);

        //TODO: Move the AudioClip and AudioSource to the audio manager.
        //PauseMenuManager.m_pauseMenuManager.PauseMenuAudioSource.PlayOneShot(m_menuClickAudioClip);

        switch (a_strParameter)
        {
            // Main panel start.
            case "Main_Panel_Continue":
                {

                    break;
                }

            case "Main_Panel_Options":
                {

                    break;
                }

            case "Main_Panel_Quit_To_Main_Menu":
                {

                    break;
                }

            case "Main_Panel_Quit_To_Desktop":
                {

                    break;
                }
            // Main panel end.

            // Options main panel start.
            case "Options_Main_Panel_Audio":
                {

                    break;
                }

            case "Options_Main_Panel_General":
                {

                    break;
                }

            case "Options_Main_Panel_Back":
                {

                    break;
                }
            // Options main panel end.

            // Options audio panel start.
            case "Options_Audio_Panel_Back":
                {

                    break;
                }
            // Options audio panel end.

            // Options general panel start.
            case "Options_General_Show_Hide_Cursor":
                {

                    break;
                }

            case "Options_General_Back":
                {

                    break;
                }
            // Options general panel end.

            // Quit to main menu panel start.
            case "Quit_To_Main_Menu_Yes":
                {

                    break;
                }

            case "Quit_To_Main_Menu_No":
                {

                    break;
                }
            // Quit to main menu panel end.

            // Quit to desktop panel start.
            case "Quit_To_Desktop_Panel_Yes":
                {

                    break;
                }

            case "Quit_To_Desktop_Panel_No":
                {

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

    private void SelectNewButton(int a_iParentListIndex)
    {
        PauseMenuManager.m_pauseMenuManager.SelectedButtonIndex = a_iParentListIndex;
        PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = false;
        PauseMenuManager.m_pauseMenuManager.SelectedButton = PauseMenuManager.m_pauseMenuManager.ActivePanelButtons[a_iParentListIndex];
        PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = true;
    }

    private void MainPanelContinue()
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
    }

    private void MainPanelOptions()
    {
        PauseMenuManager.m_pauseMenuManager.ActivePanelButtons = PauseMenuManager.m_pauseMenuManager.OptionsMainPanelButtons;
        PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = false;
        PauseMenuManager.m_pauseMenuManager.SelectedButton = PauseMenuManager.m_pauseMenuManager.ActivePanelButtons[0];
        PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = true;
        PauseMenuManager.m_pauseMenuManager.m_mainPanel.SetActive(false);
        PauseMenuManager.m_pauseMenuManager.m_optionsPanel.SetActive(true);
        PauseMenuManager.m_pauseMenuManager.m_quitToMainMenuPanel.SetActive(false);
        PauseMenuManager.m_pauseMenuManager.m_quitToDesktopPanel.SetActive(false);
        PauseMenuManager.m_pauseMenuManager.ResetSelectedButtonIndex();
    }

    private void MainPanelQuitToMainMenu()
    {
        PauseMenuManager.m_pauseMenuManager.ActivePanelButtons = PauseMenuManager.m_pauseMenuManager.QuitToMainMenuPanelButtons;
        PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = false;
        PauseMenuManager.m_pauseMenuManager.SelectedButton = PauseMenuManager.m_pauseMenuManager.ActivePanelButtons[0];
        PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = true;
        PauseMenuManager.m_pauseMenuManager.m_mainPanel.SetActive(false);
        PauseMenuManager.m_pauseMenuManager.m_optionsPanel.SetActive(false);
        PauseMenuManager.m_pauseMenuManager.m_quitToMainMenuPanel.SetActive(true);
        PauseMenuManager.m_pauseMenuManager.m_quitToDesktopPanel.SetActive(false);
        PauseMenuManager.m_pauseMenuManager.ResetSelectedButtonIndex();
    }

    private void MainPanelQuitToDesktop()
    {
        PauseMenuManager.m_pauseMenuManager.ActivePanelButtons = PauseMenuManager.m_pauseMenuManager.QuitToDesktopPanelButtons;
        PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = false;
        PauseMenuManager.m_pauseMenuManager.SelectedButton = PauseMenuManager.m_pauseMenuManager.ActivePanelButtons[0];
        PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = true;
        PauseMenuManager.m_pauseMenuManager.m_mainPanel.SetActive(false);
        PauseMenuManager.m_pauseMenuManager.m_optionsPanel.SetActive(false);
        PauseMenuManager.m_pauseMenuManager.m_quitToMainMenuPanel.SetActive(false);
        PauseMenuManager.m_pauseMenuManager.m_quitToDesktopPanel.SetActive(true);
        PauseMenuManager.m_pauseMenuManager.ResetSelectedButtonIndex();
    }

    private void OptionsMainPanelAudio()
    {
        PauseMenuManager.m_pauseMenuManager.ActivePanelButtons = PauseMenuManager.m_pauseMenuManager.OptionsAudioPanelButtons;
        PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = false;
        PauseMenuManager.m_pauseMenuManager.SelectedButton = PauseMenuManager.m_pauseMenuManager.ActivePanelButtons[0];
        PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = true;
        PauseMenuManager.m_pauseMenuManager.OptionsMainPanel.SetActive(false);
        PauseMenuManager.m_pauseMenuManager.OptionsAudioPanel.SetActive(true);
        PauseMenuManager.m_pauseMenuManager.OptionsGeneralPanel.SetActive(false);
        PauseMenuManager.m_pauseMenuManager.ResetSelectedButtonIndex();
    }

    private void OptionsMainPanelGeneral()
    {
        PauseMenuManager.m_pauseMenuManager.ActivePanelButtons = PauseMenuManager.m_pauseMenuManager.OptionsGeneralPanelButtons;
        PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = false;
        PauseMenuManager.m_pauseMenuManager.SelectedButton = PauseMenuManager.m_pauseMenuManager.ActivePanelButtons[0];
        PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = true;
        PauseMenuManager.m_pauseMenuManager.OptionsMainPanel.SetActive(false);
        PauseMenuManager.m_pauseMenuManager.OptionsAudioPanel.SetActive(false);
        PauseMenuManager.m_pauseMenuManager.OptionsGeneralPanel.SetActive(true);
        PauseMenuManager.m_pauseMenuManager.ResetSelectedButtonIndex();
    }

    private void OptionsMainPanelBack()
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
    }

    private void OptionsAudioPanelBack()
    {
        SerializationManager.m_serializationManager.Save();
        PauseMenuManager.m_pauseMenuManager.ActivePanelButtons = PauseMenuManager.m_pauseMenuManager.OptionsMainPanelButtons;
        PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = false;
        PauseMenuManager.m_pauseMenuManager.SelectedButton = PauseMenuManager.m_pauseMenuManager.ActivePanelButtons[0];
        PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = true;
        PauseMenuManager.m_pauseMenuManager.OptionsMainPanel.SetActive(true);
        PauseMenuManager.m_pauseMenuManager.OptionsAudioPanel.SetActive(false);
        PauseMenuManager.m_pauseMenuManager.ResetSelectedButtonIndex();
    }

    private void OptionsGeneralPanelShowHideCursor()
    {
        if (GameManager.m_gameManager.ForceHideCursor)
        {
            GameManager.m_gameManager.ForceHideCursor = false;
            m_button.GetComponent<Image>().sprite = PauseMenuManager.m_pauseMenuManager.m_hideCursorSprite;
            m_button.spriteState = PauseMenuManager.m_pauseMenuManager.m_hideCursorSpriteState;

        }
        else
        {
            GameManager.m_gameManager.ForceHideCursor = true;
            m_button.GetComponent<Image>().sprite = PauseMenuManager.m_pauseMenuManager.m_showCursorSprite;
            m_button.spriteState = PauseMenuManager.m_pauseMenuManager.m_showCursorSpriteState;
        }
    }

    private void OptionsGeneralBack()
    {
        PauseMenuManager.m_pauseMenuManager.ActivePanelButtons = PauseMenuManager.m_pauseMenuManager.OptionsMainPanelButtons;
        PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = false;
        PauseMenuManager.m_pauseMenuManager.SelectedButton = PauseMenuManager.m_pauseMenuManager.ActivePanelButtons[0];
        PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = true;
        PauseMenuManager.m_pauseMenuManager.OptionsMainPanel.SetActive(true);
        PauseMenuManager.m_pauseMenuManager.OptionsGeneralPanel.SetActive(false);
        PauseMenuManager.m_pauseMenuManager.ResetSelectedButtonIndex();
    }

    private void QuitToMainMenuYes()
    {
        GameManager.m_gameManager.LoadMainMenu();
    }

    private void QuitToMainMenuNo()
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
    }

    private void QuitToDesktopPanelYes()
    {
        GameManager.m_gameManager.QuitToDesktop();
    }

    private void QuitToDesktopPanelNo()
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
    }
}
