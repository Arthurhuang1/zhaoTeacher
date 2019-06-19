
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 前刮板输送机
/// </summary>
class ScraperConveyor_Front : CoalYardDevice
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void ResDevice()
    {
        CoalYardDeviceFactory.ResDevice("ScraperConveyor_Front", delegate () { return new ScraperConveyor_Front(); });
    }
    public ScraperConveyor_Front()
    {
       info. deviceTypeName = "ScraperConveyor_Front";
    }
}
/// <summary>
/// 后刮板输送机
/// </summary>
class ScraperConveyor_Back : CoalYardDevice
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void ResDevice()
    {
        CoalYardDeviceFactory.ResDevice("ScraperConveyor_Back", delegate () { return new ScraperConveyor_Back(); });
    }
    public ScraperConveyor_Back()
    {
        info.deviceTypeName = "ScraperConveyor_Back";
    }
}
