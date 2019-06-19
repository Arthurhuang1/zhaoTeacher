
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 皮带机
/// </summary>
class BeltConveyor : CoalYardDevice
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void ResDevice()
    {
        CoalYardDeviceFactory.ResDevice("BeltConveyor", delegate () { return new BeltConveyor(); });
    }

   

    public BeltConveyor()
    {
        info.deviceTypeName = "BeltConveyor";
    }
  
}
