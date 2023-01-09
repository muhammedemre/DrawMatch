using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSuccessOfficer : MonoBehaviour
{
    [SerializeField] LevelActor levelActor;
    public void LevelSuccessProcess() 
    {
        levelActor.drawArea.SetActive(false);
        levelActor.fakeDrawArea.SetActive(false);
        levelActor.successCheck.SetActive(true);
        ShootConfetties();
        UIManager.instance.uICanvasOfficer.MidButtonHandle(false);
        DisplaySuccessImage();
        AudioManager.instance.PlayASound("success");
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
        levelActor.levelQuestionImage.sprite = levelSprite;
    }
}
