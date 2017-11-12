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

        AudioManager.m_audioManager.PlayOneShotMenuClick();
        MainMenuManager.m_mainMenuManager.LastSelectedButtonIndex = MainMenuManager.m_mainMenuManager.SelectedButtonIndex;

        switch (a_strParameter)
        {
            case "MainPanelPlay":
                {
                    MainMenuManager.m_mainMenuManager.ActivePanelButtons = MainMenuManager.m_mainMenuManager.PlayPanelButtons;
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = false;
                    MainMenuManager.m_mainMenuManager.SelectedButton = MainMenuManager.m_mainMenuManager.ActivePanelButtons[0];
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = true;
                    MainMenuManager.m_mainMenuManager.m_mainPanel.SetActive(false);
                    MainMenuManager.m_mainMenuManager.PlayPanel.SetActive(true);
                    MainMenuManager.m_mainMenuManager.ResetSelectedButtonIndex();
                    break;
                }

            case "MainPanelOptions":
                {
                    MainMenuManager.m_mainMenuManager.ActivePanelButtons = MainMenuManager.m_mainMenuManager.OptionsMainPanelButtons;
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

            case "PlayPanelYes":
                {
                    MainMenuManager.m_mainMenuManager.m_mainPanel.SetActive(false);
                    MainMenuManager.m_mainMenuManager.m_backgroundImage.SetActive(false);
                    MainMenuManager.m_mainMenuManager.PlayPanel.SetActive(false);
                    MainMenuManager.m_mainMenuManager.PlayTutorial = true;
                    MainMenuManager.m_mainMenuManager.FadeIn = false;
                    AudioManager.m_audioManager.FadeIn = false;
                    break;
                }

            case "PlayPanelNo":
                {
                    MainMenuManager.m_mainMenuManager.m_mainPanel.SetActive(false);
                    MainMenuManager.m_mainMenuManager.m_backgroundImage.SetActive(false);
                    MainMenuManager.m_mainMenuManager.PlayPanel.SetActive(false);
                    MainMenuManager.m_mainMenuManager.PlayTutorial = false;
                    MainMenuManager.m_mainMenuManager.FadeIn = false;
                    AudioManager.m_audioManager.FadeIn = false;
                    break;
                }

            case "PlayPanelBack":
                {
                    MainMenuManager.m_mainMenuManager.ActivePanelButtons = MainMenuManager.m_mainMenuManager.MainPanelButtons;
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = false;
                    MainMenuManager.m_mainMenuManager.SelectedButton = MainMenuManager.m_mainMenuManager.ActivePanelButtons[0];
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = true;
                    MainMenuManager.m_mainMenuManager.m_mainPanel.SetActive(true);
                    MainMenuManager.m_mainMenuManager.PlayPanel.SetActive(false);
                    MainMenuManager.m_mainMenuManager.ResetSelectedButtonIndex();
                    break;
                }

            // Options start.
            // Options main panel start.
            case "Options_Main_Panel_Audio":
                {
                    SerializationManager.m_serializationManager.Load();
                    MainMenuManager.m_mainMenuManager.ActivePanelButtons = MainMenuManager.m_mainMenuManager.OptionsAudioPanelButtons;
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = false;
                    MainMenuManager.m_mainMenuManager.SelectedButton = MainMenuManager.m_mainMenuManager.ActivePanelButtons[0];
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = true;
                    MainMenuManager.m_mainMenuManager.OptionsMainPanel.SetActive(false);
                    MainMenuManager.m_mainMenuManager.OptionsAudioPanel.SetActive(true);
                    MainMenuManager.m_mainMenuManager.OptionsGeneralPanel.SetActive(false);
                    MainMenuManager.m_mainMenuManager.ResetSelectedButtonIndex();
                    break;
                }

            case "Options_Main_Panel_General":
                {
                    MainMenuManager.m_mainMenuManager.ActivePanelButtons = MainMenuManager.m_mainMenuManager.OptionsGeneralPanelButtons;
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = false;
                    MainMenuManager.m_mainMenuManager.SelectedButton = MainMenuManager.m_mainMenuManager.ActivePanelButtons[0];
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = true;
                    MainMenuManager.m_mainMenuManager.OptionsMainPanel.SetActive(false);
                    MainMenuManager.m_mainMenuManager.OptionsAudioPanel.SetActive(false);
                    MainMenuManager.m_mainMenuManager.OptionsGeneralPanel.SetActive(true);
                    MainMenuManager.m_mainMenuManager.ResetSelectedButtonIndex();
                    break;
                }

            case "Options_Main_Panel_Back":
                {
                    MainMenuManager.m_mainMenuManager.ActivePanelButtons = MainMenuManager.m_mainMenuManager.MainPanelButtons;
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = false;
                    MainMenuManager.m_mainMenuManager.SelectedButton = MainMenuManager.m_mainMenuManager.ActivePanelButtons[0];
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = true;
                    MainMenuManager.m_mainMenuManager.m_mainPanel.SetActive(true);
                    MainMenuManager.m_mainMenuManager.m_optionsPanel.SetActive(false);
                    MainMenuManager.m_mainMenuManager.m_quitToDesktopPanel.SetActive(false);
                    MainMenuManager.m_mainMenuManager.ResetSelectedButtonIndex();
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
            // Options main panel end.

            // Options audio panel start.
            case "Options_Audio_Panel_Back":
                {
                    SerializationManager.m_serializationManager.Save();
                    MainMenuManager.m_mainMenuManager.ActivePanelButtons = MainMenuManager.m_mainMenuManager.OptionsMainPanelButtons;
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = false;
                    MainMenuManager.m_mainMenuManager.SelectedButton = MainMenuManager.m_mainMenuManager.ActivePanelButtons[0];
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = true;
                    MainMenuManager.m_mainMenuManager.OptionsMainPanel.SetActive(true);
                    MainMenuManager.m_mainMenuManager.OptionsAudioPanel.SetActive(false);
                    MainMenuManager.m_mainMenuManager.ResetSelectedButtonIndex();
                    break;
                }
            // Options audio panel end.

            // Options general panel start.
            case "Options_General_Show_Hide_Cursor":
                {
                    if (GameManager.m_gameManager.ForceHideCursor)
                    {
                        GameManager.m_gameManager.ForceHideCursor = false;
                        m_button.GetComponent<Image>().sprite = MainMenuManager.m_mainMenuManager.m_hideCursorSprite;
                        m_button.spriteState = MainMenuManager.m_mainMenuManager.m_hideCursorSpriteState;
                    }
                    else
                    {
                        GameManager.m_gameManager.ForceHideCursor = true;
                        m_button.GetComponent<Image>().sprite = MainMenuManager.m_mainMenuManager.m_showCursorSprite;
                        m_button.spriteState = MainMenuManager.m_mainMenuManager.m_showCursorSpriteState;
                    }
                    break;
                }

            case "Options_General_Back":
                {
                    MainMenuManager.m_mainMenuManager.ActivePanelButtons = MainMenuManager.m_mainMenuManager.OptionsMainPanelButtons;
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = false;
                    MainMenuManager.m_mainMenuManager.SelectedButton = MainMenuManager.m_mainMenuManager.ActivePanelButtons[0];
                    MainMenuManager.m_mainMenuManager.SelectedButton.IsMousedOver = true;
                    MainMenuManager.m_mainMenuManager.OptionsMainPanel.SetActive(true);
                    MainMenuManager.m_mainMenuManager.OptionsGeneralPanel.SetActive(false);
                    MainMenuManager.m_mainMenuManager.ResetSelectedButtonIndex();
                    break;
                }
            // Options general panel end.

            // Options end.
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
