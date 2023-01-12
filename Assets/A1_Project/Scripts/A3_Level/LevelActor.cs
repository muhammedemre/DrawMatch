using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelActor : MonoBehaviour
{
    public LevelPreparationOfficer levelPreparationOfficer;
    public LevelCollectedPointsOfficer levelCollectedPointsOfficer;
    public LevelSuccessOfficer levelSuccessOfficer;
    public Image levelQuestionImage, levelSolvedImage, levelHintImage;
    public int levelIndex;
    public GameObject drawArea, successCheck;

    public void LevelIsSuccessfullyCompleted() 
    {
        levelSuccessOfficer.LevelSuccessProcess();
    }

    public void LevelCompleteTryIsFailed()
    {
        levelSuccessOfficer.LevelFailProcess();
    }

    public void RevealTheHint()
    {
        levelHintImage.gameObject.SetActive(true);
        string spritePath = "LevelHintSprites/" + "LEVEL-" + levelIndex.ToString()+"-HINT";
        Sprite levelHintSprite = Resources.Load<Sprite>(spritePath);
        levelHintImage.sprite = levelHintSprite;
    }
    
}
