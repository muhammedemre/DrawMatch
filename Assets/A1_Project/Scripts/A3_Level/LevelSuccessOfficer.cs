using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSuccessOfficer : MonoBehaviour
{
    [SerializeField] LevelActor levelActor;
    public void LevelSuccessProcess() 
    {
        levelActor.drawArea.SetActive(false);
        levelActor.successCheck.SetActive(true);
        ShootConfetties();
        UIManager.instance.uICanvasOfficer.MidButtonHandle(false);
        DisplaySuccessImage();
        AudioManager.instance.PlayASound("success");
        VibrationManager.instance.Vibrate(true);
    }

    public void LevelFailProcess()
    {
        VibrationManager.instance.Vibrate(false);
    }

    void ShootConfetties()
    {
        foreach (ParticleSystem confetti in UIManager.instance.uICanvasOfficer.confettieList)
        {
            confetti.Play();
        }
    }

    void DisplaySuccessImage() 
    {
        string spritePath = "LevelSprites/" + "LEVEL-" + levelActor.levelIndex.ToString()+"-TRUE";
        Sprite levelSprite = Resources.Load<Sprite>(spritePath);
        levelActor.levelQuestionImage.gameObject.SetActive(false);
        levelActor.levelHintImage.gameObject.SetActive(false);
        levelActor.levelSolvedImage.sprite = levelSprite;
        levelActor.levelSolvedImage.gameObject.SetActive(true);
        //levelActor.levelSolvedImage.GetComponent<Animator>().enabled = true;
    }
}
