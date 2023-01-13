using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FaceBookSDKManager : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectsOfType<FaceBookSDKManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(transform.gameObject);

        Application.targetFrameRate = 60;
        //if (Application.platform == RuntimePlatform.IPhonePlayer)
        //{
        //    Application.targetFrameRate = 60;
        //}

        FB.Init(FBInitCallback);
        //GameAnalytics.Initialize();
    }

    private void FBInitCallback()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
    }

    public void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
        }
    }

}
