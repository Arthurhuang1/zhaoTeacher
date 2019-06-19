using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSwitcher : MonoBehaviour
{
    private static ControllerSwitcher instance = null;
    public  static ControllerSwitcher GetInstance() { return instance; }
    private static ControllerBase currentController = null;


    private void Awake()
    {
        instance = this;
        
    }

	
	
    public static void SwitchingController(ControllerBase ct)
    {
        DisableCurrentController();
        EnableController(ct);


    }
    public static void DisableCurrentController()
    {
        if (currentController != null && currentController.IsEnableController)
        {
            currentController.DisableController();
        }
    }
    public static void EnableController(ControllerBase ct)
    {
        if (ct != null)
        {
            ct.EnableController();
        }
    }

    
    
}
