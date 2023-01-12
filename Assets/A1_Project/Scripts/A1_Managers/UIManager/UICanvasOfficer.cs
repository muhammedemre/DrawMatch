using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvasOfficer : MonoBehaviour
{
    public SplashScreenActor splashScreenActor;
    public SettingsActor settingsActor;
    public GameObject inGameScreen, nextButton, hintButton, bgMusicButton, soundButton, vibrationButton;
    public float splashScreenDuration;
    public List<ParticleSystem> confettieList = new List<ParticleSystem>();


    public void DisplaySplashScreen() 
    {
        splashScreenActor.SplashProcess(splashScreenDuration, () => AfterSplashScreenProcess());
    }

    void AfterSplashScreenProcess() 
    {
        ActivateInGameScreen();
        MidButtonHandle(true);
        GameManager.instance.gameManagerObserverOfficer.Publish(ObserverSubjects.LevelInstantiate);

    }

    void ActivateInGameScreen()
    {
        splashScreenActor.gameObject.SetActive(false);
        inGameScreen.SetActive(true);
    }

    public void MidButtonHandle(bool hint) // next or Hint button
    {
        hintButton.SetActive(hint);
        nextButton.SetActive(!hint);
    }
}
