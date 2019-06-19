using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 乳化泵
/// </summary>
public  class EmulsionPump:CoalYardDevice
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void ResDevice()
    {
        CoalYardDeviceFactory.ResDevice("EmulsionPump", delegate() { return new EmulsionPump(); });
    }
    public EmulsionPump()
    {
        info.deviceTypeName = "EmulsionPump";
    }
 
    
}
