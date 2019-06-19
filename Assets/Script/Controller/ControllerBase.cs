using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControllerBase:MonoBehaviour
{

    private bool isEnableController = false;

    public bool IsEnableController { get { return isEnableController; } }
    public void EnableController()
    {
        isEnableController = true;
        enabled = isEnableController;
    }
    public void DisableController()
    {
        isEnableController = false;
        enabled = isEnableController;
    }



  
}
