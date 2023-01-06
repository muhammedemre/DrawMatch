using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMoveOfficer : MonoBehaviour
{
    public void GoNextLevel()
    {
        if (LevelManager.instance.levelAmount > LevelManager.instance.levelCreateOfficer.levelCounter)
        {
            Destroy(LevelManager.instance.levelCreateOfficer.currentLevel.gameObject);
            LevelManager.instance.levelCreateOfficer.levelCounter++;
            LevelManager.instance.levelCreateOfficer.CreateLevelProcess();
        }
        
    }
    public void GoPreviousLevel() 
    {
        if (1 < LevelManager.instance.levelCreateOfficer.levelCounter)
        {
            Destroy(LevelManager.instance.levelCreateOfficer.currentLevel.gameObject);
            LevelManager.instance.levelCreateOfficer.levelCounter--;
            LevelManager.instance.levelCreateOfficer.CreateLevelProcess();
        }
    }
}
