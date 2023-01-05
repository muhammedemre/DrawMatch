using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelActor : MonoBehaviour
{
    public LevelPreparationOfficer levelPreparationOfficer;
    public LevelCollectedPointsOfficer levelCollectedPointsOfficer;
    public Image levelQuestionImage, levelSolvedImage;
    public int levelIndex;
}
