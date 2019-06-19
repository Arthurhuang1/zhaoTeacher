using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 清水泵
/// </summary>
class ClarifiedWaterPump : CoalYardDevice
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void ResDevice()
    {
        CoalYardDeviceFactory.ResDevice("ClarifiedWaterPump", delegate () { return new ClarifiedWaterPump(); });
    }
    public ClarifiedWaterPump()
    {
        info.deviceTypeName = "ClarifiedWaterPump";
    }
}
