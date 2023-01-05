using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPreparationOfficer : MonoBehaviour
{
    [SerializeField] LevelActor levelActor;
    [SerializeField] float afterReadyDelay;

    private void Start()
    {
        PrepareTheLevel();
    }

    void PrepareTheLevel()
    {

        AssignTheLevelImages(levelActor.levelIndex);
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
}
