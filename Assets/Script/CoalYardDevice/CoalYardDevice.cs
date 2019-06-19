using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public struct CoalYardDeviceInfo
{
    public string deviceTypeName ;
    public int deviceId ;
}

/// <summary>
/// 煤场设备
/// </summary>
public class CoalYardDevice
{
   public CoalYardDeviceInfo info;
    public static List<CoalYardDeviceInfo> alreadyStartedDevice = new List<CoalYardDeviceInfo>();

    public static void CloseAlreadyStartedDevice(float interval,Action end)
    {
       // GlobalBehaviour.StartGlobalCoroutine(IECloseAlreadyStartedDevice(interval, end));
        InOut16.CloseAlreadyStartedDevice();
        end();
    }
  
    private static IEnumerator IECloseAlreadyStartedDevice(float interval, Action end)
    {
        List<CoalYardDeviceInfo> templist = new List<CoalYardDeviceInfo>();
        for (int i = 0; i < alreadyStartedDevice.Count; i++)
        {
            templist.Add( alreadyStartedDevice[i]);
        }
        for (int i = 0; i < templist.Count; i++)
        {
            DeviceSwitchgearSlider.SetSliderValueByDeviceSwitchgear(templist[i].deviceTypeName, templist[i].deviceId, false);
            OnDeviceSwitchgear(templist[i].deviceTypeName, templist[i].deviceId,false);
            yield return new WaitForSeconds(interval);
        }
        end();
        yield return null;
    }

    public static void OnDeviceSwitchgear(string dname, int id, bool isopen)
    {
        CoalYardDeviceInfo info;
        info.deviceTypeName = dname;
        info.deviceId = id;
        if (isopen)
        {
            if (alreadyStartedDevice.Contains(info))
            {
                Debug.Log("异常: 设备已经开启");
            }
            else
            {
                alreadyStartedDevice.Add(info);
            }
        }
        else
        {
            if (alreadyStartedDevice.Contains(info))
            {
                alreadyStartedDevice.Remove(info);
            }
            else
            {
                Debug.Log("异常: 设备未开启过");
            }
        }
    
        InOut16.OnDeviceSwitchgear(dname, id, isopen);
    }

};


/// <summary>
/// 采煤机
/// </summary>
class CoalCutter : CoalYardDevice
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void ResDevice()
    {
        CoalYardDeviceFactory.ResDevice("CoalCutter", delegate () { return new CoalCutter(); });
    }
    public CoalCutter()
    {
        info.deviceTypeName = "CoalCutter";
    }
}


/*
* 乳化泵
* 清水泵
* 皮带机
* 破碎机
* 转运机
* 前刮板运输机
* 后刮板运输机
* 采煤机
* 
*/
