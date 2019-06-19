
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 破碎机
/// </summary>
class Crusher : CoalYardDevice
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void ResDevice()
    {
        CoalYardDeviceFactory.ResDevice("Crusher", delegate () { return new Crusher(); });
    }
    public Crusher()
    {
        info.deviceTypeName = "Crusher";
    }
}
