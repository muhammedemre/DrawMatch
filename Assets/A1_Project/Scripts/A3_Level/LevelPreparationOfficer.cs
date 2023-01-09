using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelPreparationOfficer : MonoBehaviour
{
    [SerializeField] LevelActor levelActor;
    [SerializeField] float afterReadyDelay;
    [SerializeField] TextMeshProUGUI levelText;

    private void Start()
    {
        PrepareTheLevel();
    }

    void PrepareTheLevel()
    {
        AssignTheLevelImages(levelActor.levelIndex);
        AssignTheLevel();
        StartCoroutine(LevelIsReadyDelay());
    }

    IEnumerator LevelIsReadyDelay()
    {
        yield return new WaitForSeconds(afterReadyDelay);
        GameManager.instance.gameManagerObserverOfficer.Publish(ObserverSubjects.PostLevelInstantiate);
    }

    void AssignTheLevelImages(int levelIndex)
    {      
        string spritePath = "LevelSprites/" + "LEVEL-" + levelIndex.ToString();
        Sprite levelSprite = Resources.Load<Sprite>(spritePath);
        levelActor.levelQuestionImage.sprite = levelSprite;
    }

    void AssignTheLevel() 
    {
        levelText.text = "Level "+levelActor.levelIndex.ToString();
    }
}
