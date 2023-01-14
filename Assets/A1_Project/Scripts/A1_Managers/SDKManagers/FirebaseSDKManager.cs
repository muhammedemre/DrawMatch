using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
//using Firebase;
//using Firebase.Extensions;

public class FirebaseSDKManager : MonoBehaviour
{
    public static bool isInitialized = false;
    public static UnityEvent OnInitialized = new UnityEvent();

    private void Awake()
    {
        if (FindObjectsOfType<FirebaseSDKManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        //FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        //{
        //    var app = FirebaseApp.DefaultInstance;
        //    isInitialized = true;
        //    OnInitialized?.Invoke();
        //});
    }
}
