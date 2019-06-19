using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Homepage : MonoBehaviour {

    public static Panel_Homepage Instance { get; private set; }

    public Button startupButton;
    public Button scramButton;
    public LerpImageColor earlyWarningImage;

    public Win_DeviceParameterList deviceParameterWindows;
    private bool isStartup = false;
    private bool isEarlyWarning = false;
    private bool isUrgentStopInProgress = false;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InOut16.instance.onUpdateIn16Out16 = OnUpdateIn16Out16;
    }
    public void OnUpdateIn16Out16(byte ih,byte il,byte oh,byte ol)
    {
        DeviceSwitchgearSlider.SetSliderValueByInOutID(oh,ol);
    }
    public void OnClickSetting()
    {
        PanelSwitcher.OpenSetting();
      
    }
     
    public void OnClickWarning()
    {
        isEarlyWarning = !isEarlyWarning;
        if (isEarlyWarning)
        {
            OnDeviceSwitchgear("Warning", 0, true);
            earlyWarningImage.Open();
        }
        else
        {
            OnDeviceSwitchgear("Warning", 0, false);
            earlyWarningImage.Close(true);

        }
    }
    public void OnClickStartupDevice()
    {
        if (GameMode.StartupProcess.IsInExecution)
        {
            Debug.LogError("程序执行中...");
            return;
        }
        if (isStartup)
        {
            CloseDevice();
        }
        else
        {
            StartupDevice();
        }
        isStartup = !isStartup;
    }
    void StartupDevice()
    {
        startupButton.GetComponentInChildren<Text>().text = "关闭";
        startupButton.GetComponentInChildren<Image>().color = Color.red;

        if (GameMode.StartupProcess!=null)
        {
            GameMode.StartupProcess.ExeProcedure();
        }

    }
    void CloseDevice()
    {
        startupButton.GetComponentInChildren<Text>().text = "启动";
        startupButton.GetComponentInChildren<Image>().color = Color.white;

        if (GameMode.StartupProcess != null)
        {
            GameMode.StartupProcess.ReverseExeProcedure();
        }
    }


    public void OnClickScram()
    {
        if (isUrgentStopInProgress)//紧急停止中
        {
            Debug.Log("紧急停止中");
            return;
        }

        isUrgentStopInProgress = true;
        scramButton.interactable = false;
        CoalYardDevice.CloseAlreadyStartedDevice(0.2f,delegate() { isUrgentStopInProgress = false; scramButton.interactable = true; });

       // CloseAlreadyStartedDevice
    }
    public static void OnDeviceSwitchgear(string dname, int id, bool isopen)
    {
        CoalYardDevice.OnDeviceSwitchgear(dname, id, isopen);
    }
    public static void OnShowDeviceParameter(string dname, string chname,int id)
    {
        Instance.deviceParameterWindows.Open(chname+id.ToString(),GetDeviceParameter(dname,id),GetDeviceErrorParameter(dname,id));
    }
    static List<DeviceParameter> GetDeviceParameter(string dname, int id) {
        List<DeviceParameter> res = new List<DeviceParameter>();
        switch (dname) {
            case "BeltConveyor":// 皮带机
                res.Add(new DeviceParameter("BeltConveyor", "轴温1", 1));
                res.Add(new DeviceParameter("BeltConveyor", "轴温2", 1));
                res.Add(new DeviceParameter("BeltConveyor", "油温", 1));
                res.Add(new DeviceParameter("BeltConveyor", "电机绕组温度", 1));
                res.Add(new DeviceParameter("BeltConveyor", "滚筒温度", 1));
                res.Add(new DeviceParameter("BeltConveyor", "纵撕", 1));
                res.Add(new DeviceParameter("BeltConveyor", "堆煤", 1));
                res.Add(new DeviceParameter("BeltConveyor", "跑偏1", 1));
                res.Add(new DeviceParameter("BeltConveyor", "跑偏2", 1));
                res.Add(new DeviceParameter("BeltConveyor", "跑偏3", 1));
                res.Add(new DeviceParameter("BeltConveyor", "跑偏4", 1));
                res.Add(new DeviceParameter("BeltConveyor", "跑偏5", 1));
                res.Add(new DeviceParameter("BeltConveyor", "跑偏6", 1));
                res.Add(new DeviceParameter("BeltConveyor", "跑偏7", 1));
                res.Add(new DeviceParameter("BeltConveyor", "跑偏8", 1));
                res.Add(new DeviceParameter("BeltConveyor", "跑偏9", 1));
                res.Add(new DeviceParameter("BeltConveyor", "跑偏10", 1));
                res.Add(new DeviceParameter("BeltConveyor", "皮带机速度", 0.6f, DeviceParameter.ValueType.AnalogQuantity));
                res.Add(new DeviceParameter("BeltConveyor", "工作电流", 0.7f, DeviceParameter.ValueType.AnalogQuantity));
                res.Add(new DeviceParameter("BeltConveyor", "胶带张力", 0.8f, DeviceParameter.ValueType.AnalogQuantity));
                break;
            case "ReversedLoader"://转运机
                res.Add(new DeviceParameter("ReversedLoader", "转运机转速", 0.8f, DeviceParameter.ValueType.AnalogQuantity));
                break;

            case "ScraperConveyor_Front"://前刮板输送机
                res.Add(new DeviceParameter("ScraperConveyor_Front", "运输机转速", 0.8f, DeviceParameter.ValueType.AnalogQuantity));
                break;
            case "ScraperConveyor_Back"://前刮板输送机
                res.Add(new DeviceParameter("ScraperConveyor_Back", "运输机转速", 0.66f, DeviceParameter.ValueType.AnalogQuantity));
                break;
            case "Temperature"://环境温度
                res.Add(new DeviceParameter("Temperature", "环境温度", 0.66f, DeviceParameter.ValueType.AnalogQuantity));
                break;
            case "Smoke"://烟雾
                for (int i = 1; i <= 10; i++) {
                    res.Add(new DeviceParameter("Smoke", "烟雾" + i.ToString(), 1));
                }
                break;
            case "Mashgas"://瓦斯浓度
                res.Add(new DeviceParameter("Mashgas", "瓦斯浓度", 0.76f, DeviceParameter.ValueType.AnalogQuantity));
                break;
            case "CarbonMonoxide"://一氧化碳浓度
                res.Add(new DeviceParameter("CarbonMonoxide", "一氧化碳浓度", 0.86f, DeviceParameter.ValueType.AnalogQuantity));
                break;
        }
        return res;
    }
    static List<DeviceParameter>  GetDeviceErrorParameter(string dname, int id)
    {
        return null;
    }


}
