﻿public class LevelManager : Manager
{
    public static LevelManager instance;
    public LevelCreateOfficer levelCreateOfficer;
    public LevelMoveOfficer levelMoveOfficer;
    public int levelAmount;

    private void Awake()
    {
        SingletonCheck();
    }
    
    void SingletonCheck()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    public override void LevelInstantiateProcess()
    {
        levelCreateOfficer.CreateLevelProcess();
    }


}
