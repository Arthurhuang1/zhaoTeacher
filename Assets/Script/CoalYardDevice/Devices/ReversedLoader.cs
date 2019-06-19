
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 转运机
/// </summary>
class ReversedLoader : CoalYardDevice
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void ResDevice()
    {
        CoalYardDeviceFactory.ResDevice("ReversedLoader", delegate () { return new ReversedLoader(); });
    }
    public ReversedLoader()
    {
        info.deviceTypeName = "ReversedLoader";
    }
}
