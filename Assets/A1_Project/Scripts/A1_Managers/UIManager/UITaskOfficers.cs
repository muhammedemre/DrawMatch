using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}
