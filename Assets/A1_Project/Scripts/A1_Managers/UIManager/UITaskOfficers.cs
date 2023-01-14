using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;

public class UITaskOfficers : MonoBehaviour
{
    
    public void ReplayButton()
    {
        SceneManager.LoadScene(0);
    }

    public void SettingsOpen() 
    {
        UIManager.instance.uICanvasOfficer.settingsActor.PlayOpen();
    }

    public void SettingsClose() 
    {
        UIManager.instance.uICanvasOfficer.settingsActor.PlayClose();
    }

    public void AdminCheatNextLevel(bool next) 
    {
        if (next)
        {
            LevelManager.instance.levelMoveOfficer.GoNextLevel();
        }
        else
        {
            LevelManager.instance.levelMoveOfficer.GoPreviousLevel();
        }
    }

    public void NextButton() 
    {
        LevelManager.instance.levelMoveOfficer.GoNextLevel();
        UIManager.instance.uICanvasOfficer.nextButton.SetActive(false);
    }

    public void BGmusicStateChange() 
    {
        AudioManager.instance.ChangeMusicState();
        DataManager.instance.DataSaveAndLoadOfficer.SaveTheData();
        GameAnalytics.NewDesignEvent("bg_music_state_" + AudioManager.instance.bGmusicState.ToString());
    }
    public void SFXStateChange() 
    {
        AudioManager.instance.ChangeSFXState();
        DataManager.instance.DataSaveAndLoadOfficer.SaveTheData();
        GameAnalytics.NewDesignEvent("sfx_state_" + AudioManager.instance.soundFXState.ToString());
    }

    public void HintButton() 
    {
        // Once reklam oynatilacak ve oradan tetiklenecek;
        LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>().RevealTheHint();
        GameAnalytics.NewDesignEvent("hint_requested");

    }

    public void VibrationButton()
    {
        VibrationManager.instance.ChangeVibrationState();
        DataManager.instance.DataSaveAndLoadOfficer.SaveTheData();
        GameAnalytics.NewDesignEvent("vibration_state_" + VibrationManager.instance.ableToVibrate.ToString());

    }
}
