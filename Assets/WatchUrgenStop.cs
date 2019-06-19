using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WatchUrgenStop : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public Button scramButton;
    private bool isUrgentStopInProgress = false;
    public void OnClickScram()
    {
        if (isUrgentStopInProgress)//紧急停止中
        {
            Debug.Log("紧急停止中");
            return;
        }

        isUrgentStopInProgress = true;
        scramButton.interactable = false;
        CoalYardDevice.CloseAlreadyStartedDevice(0.2f, delegate () { isUrgentStopInProgress = false; scramButton.interactable = true; });

        // CloseAlreadyStartedDevice
    }
}
