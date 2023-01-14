using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameAnalyticsSDK;

public class LevelActor : MonoBehaviour
{
    public LevelPreparationOfficer levelPreparationOfficer;
    public LevelCollectedPointsOfficer levelCollectedPointsOfficer;
    public LevelSuccessOfficer levelSuccessOfficer;
    public Image levelQuestionImage, levelSolvedImage, levelHintImage;
    public int levelIndex;
    public GameObject drawArea, successCheck;

    void Start()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "level_" + levelIndex.ToString());
    }

    public void LevelIsSuccessfullyCompleted() 
    {
        levelSuccessOfficer.LevelSuccessProcess();
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "level_" + levelIndex.ToString());
    }

    public void LevelCompleteTryIsFailed()
    {
        levelSuccessOfficer.LevelFailProcess();
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "level_" + levelIndex.ToString());
    }

    public void RevealTheHint()
    {
        levelHintImage.gameObject.SetActive(true);
        string spritePath = "LevelHintSprites/" + "LEVEL-" + levelIndex.ToString()+"-HINT";
        Sprite levelHintSprite = Resources.Load<Sprite>(spritePath);
        levelHintImage.sprite = levelHintSprite;
    }
    
}
