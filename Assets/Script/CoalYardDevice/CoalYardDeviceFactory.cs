using System;
using System.Collections.Generic;
using UnityEngine;

public delegate CoalYardDevice CreateCoalYardDevice();
public class CoalYardDeviceFactory
{
    public static Dictionary<string, CreateCoalYardDevice> deviceTypeList = new Dictionary<string, CreateCoalYardDevice>();
    public static void ResDevice(string dname,CreateCoalYardDevice cdevfun)
    {
        if (!deviceTypeList.ContainsKey(dname))
        {
            deviceTypeList.Add(dname, cdevfun);
           // Debug.Log( dname + "注册");

        }
        else
        {
            Debug.Log("ERROR::"+dname+"重复注册");
        }
    }

    public static CoalYardDevice CreateDevice(string dname)
    {
        CoalYardDevice res = null;
        if (deviceTypeList.ContainsKey(dname))
        {
             res = deviceTypeList[dname]();
        }
        else
        {
            Debug.Log("ERROR::" + dname + "未注册");
        }

        return res;

        
    }
    public static CoalYardDevice CreateDevice(string dname, int id)
    {
        CoalYardDevice d = CreateDevice(dname);
        d.info.deviceId = id;
        return d;
    }
}
