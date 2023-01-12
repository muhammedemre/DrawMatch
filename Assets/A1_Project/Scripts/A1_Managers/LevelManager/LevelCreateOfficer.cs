using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreateOfficer : MonoBehaviour
{
    [SerializeField] private GameObject levelContainer;
    public GameObject currentLevel;

    [SerializeField] int levelCounter = 1;

    public int LevelCounter 
    {
        get 
        { 
            return levelCounter; 
        }
        set 
        {
            levelCounter = value;
            levelCounter = levelCounter % LevelManager.instance.levelAmount;
            levelCounter = levelCounter == 0 ? 1 : levelCounter;
        }
    }

    public void CreateLevelProcess()
    {
        string levelPath = "LevelPrefabs/" + "Level" + LevelCounter.ToString();
        GameObject levelPrefab = Resources.Load<GameObject>(levelPath);
        CreateTheLevel(levelPrefab);
    }

    public void CreateTheLevel(GameObject levelPrefab)
    {
        GameObject tempLevel = Instantiate(levelPrefab, levelContainer.transform.position, Quaternion.identity, levelContainer.transform);
        tempLevel.GetComponent<LevelActor>().levelIndex = LevelCounter;
        currentLevel = tempLevel;
        //currentLevel.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        GameManager.instance.gameManagerObserverOfficer.Publish(ObserverSubjects.PostGameStart);

        GameManager.instance.gameManagerObserverOfficer.Publish(ObserverSubjects.PreLevelInstantiate); // Can be arranged according to PostGameStart;
        UIManager.instance.uICanvasOfficer.MidButtonHandle(true);

        DataManager.instance.DataSaveAndLoadOfficer.SaveTheData();
    }
}
