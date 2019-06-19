using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBehaviour : MonoBehaviour {

    public static GlobalBehaviour instance = null;
    public static GlobalBehaviour GetInstance()
    {

        if (instance == null)
        {
            instance = new GameObject("GlobalBehaviour").AddComponent<GlobalBehaviour>();
        }

        return instance;
    }
   
    private void OnDestroy()
    {
        instance = null;
    }
    public static void StartGlobalCoroutine(IEnumerator tor)
    {
        GetInstance().StartCoroutine(tor);
    }
    public static void DelayedExecution(float sec, Action a)
    {
        GetInstance().StartCoroutine(CoroutineDelayedExecution(sec, a));
    }
    private static IEnumerator CoroutineDelayedExecution(float sec, Action endact)
    {
        yield return new WaitForSeconds(sec);
        endact();
    }

}
