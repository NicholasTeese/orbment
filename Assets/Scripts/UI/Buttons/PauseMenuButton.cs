using UnityEngine;
using UnityEngine.UI;

public class PauseMenuButton : BaseButton
{
    protected override void Awake()
    {
        base.Awake();

        if (m_strOnClickParameter == "Show_Hide_Cursor")
        {
            if (!GameManager.m_gameManager.ForceHideCursor)
            {
                m_button.GetComponentInChildren<Text>().text = "Force Hide Cursor";
            }
            else
            {
                m_button.GetComponentInChildren<Text>().text = "Force Show Cursor";
            }
        }
    }

    public override void OnCursorEnter()
    {
        base.OnCursorEnter();

        PauseMenuManager.m_pauseMenuManager.SelectedButtonIndex = m_iParentListIndex;
        PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = false;
        PauseMenuManager.m_pauseMenuManager.SelectedButton = PauseMenuManager.m_pauseMenuManager.ActivePanelButtons[m_iParentListIndex];
        PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = true;
    }

    public override void OnClick(string a_strParameter)
    {
        base.OnClick(a_strParameter);

        AudioManager.m_audioManager.PlayOneShotMenuClick();
        PauseMenuManager.m_pauseMenuManager.LastSelectedButtonIndex = PauseMenuManager.m_pauseMenuManager.SelectedButtonIndex;

        switch (a_strParameter)
        {
            // Main panel start.
            case "MainPanelContinue":
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
                    break;
                }

            case "MainPanelOptions":
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
                    break;
                }

            case "MainPanelQuitToMainMenu":
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
                    break;
                }

            case "MainPanelQuitToDesktop":
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
                    break;
                }
            // Main panel end.

            // Options panel start.
            // Options main panel start.
            case "Options_Main_Panel_Audio":
                {
                    PauseMenuManager.m_pauseMenuManager.ActivePanelButtons = PauseMenuManager.m_pauseMenuManager.OptionsAudioPanelButtons;
                    PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = false;
                    PauseMenuManager.m_pauseMenuManager.SelectedButton = PauseMenuManager.m_pauseMenuManager.ActivePanelButtons[0];
                    PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = true;
                    PauseMenuManager.m_pauseMenuManager.OptionsMainPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuManager.OptionsAudioPanel.SetActive(true);
                    PauseMenuManager.m_pauseMenuManager.OptionsGeneralPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuManager.ResetSelectedButtonIndex();
                    break;
                }

            case "Options_Main_Panel_General":
                {
                    PauseMenuManager.m_pauseMenuManager.ActivePanelButtons = PauseMenuManager.m_pauseMenuManager.OptionsGeneralPanelButtons;
                    PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = false;
                    PauseMenuManager.m_pauseMenuManager.SelectedButton = PauseMenuManager.m_pauseMenuManager.ActivePanelButtons[0];
                    PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = true;
                    PauseMenuManager.m_pauseMenuManager.OptionsMainPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuManager.OptionsAudioPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuManager.OptionsGeneralPanel.SetActive(true);
                    PauseMenuManager.m_pauseMenuManager.ResetSelectedButtonIndex();
                    break;
                }

            case "Options_Main_Panel_Back":
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
                    break;
                }
            // Options main panel end.

            // Options audio panel start.
            case "Options_Audio_Panel_Back":
                {
                    SerializationManager.m_serializationManager.Save();
                    PauseMenuManager.m_pauseMenuManager.ActivePanelButtons = PauseMenuManager.m_pauseMenuManager.OptionsMainPanelButtons;
                    PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = false;
                    PauseMenuManager.m_pauseMenuManager.SelectedButton = PauseMenuManager.m_pauseMenuManager.ActivePanelButtons[0];
                    PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = true;
                    PauseMenuManager.m_pauseMenuManager.OptionsMainPanel.SetActive(true);
                    PauseMenuManager.m_pauseMenuManager.OptionsAudioPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuManager.ResetSelectedButtonIndex();
                    break;
                }
            // Options audio panel end.

            // Options general panel start.
            case "Options_General_Show_Hide_Cursor":
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
                    break;
                }

            case "Options_General_Back":
                {
                    PauseMenuManager.m_pauseMenuManager.ActivePanelButtons = PauseMenuManager.m_pauseMenuManager.OptionsMainPanelButtons;
                    PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = false;
                    PauseMenuManager.m_pauseMenuManager.SelectedButton = PauseMenuManager.m_pauseMenuManager.ActivePanelButtons[0];
                    PauseMenuManager.m_pauseMenuManager.SelectedButton.IsMousedOver = true;
                    PauseMenuManager.m_pauseMenuManager.OptionsMainPanel.SetActive(true);
                    PauseMenuManager.m_pauseMenuManager.OptionsGeneralPanel.SetActive(false);
                    PauseMenuManager.m_pauseMenuManager.ResetSelectedButtonIndex();
                    break;
                }
            // Options general panel end.

            // Options panel end.

            // Quit to main menu panel start.
            case "QuitToMainMenuYes":
                {
                    GameManager.m_gameManager.LoadMainMenu();
                    break;
                }

            case "QuitToMainMenuNo":
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
                    break;
                }
            // Quit to main menu panel end.

            // Quit to desktop panel start.
            case "QuitToDesktopPanelYes":
                {
                    GameManager.m_gameManager.QuitToDesktop();
                    break;
                }

            case "QuitToDesktopPanelNo":
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

    public override void OnValueChanged(string a_strParameter)
    {
        base.OnValueChanged(a_strParameter);

        switch (a_strParameter)
        {
            case "Master_Volume":
                {
                    AudioManager.m_audioManager.AdjustMasterVolume(m_slider.value);
                    break;
                }

            case "Music_Volume":
                {
                    AudioManager.m_audioManager.AdjustMusicVolume(m_slider.value);
                    break;
                }

            case "Bullet_Volume":
                {
                    AudioManager.m_audioManager.AdjustBulletVolume(m_slider.value);
                    break;
                }

            case "Effects_Volume":
                {
                    AudioManager.m_audioManager.AdjustEffectsVolume(m_slider.value);
                    break;
                }

            case "Menu_Volume":
                {
                    AudioManager.m_audioManager.AdjustMenuVolume(m_slider.value);
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
