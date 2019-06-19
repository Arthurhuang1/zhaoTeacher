using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceSwitchgearSlider : MonoBehaviour {

    public Text showText;
    public SwitchgearSlider switchgearSlider;
    public string deviceTypeName ;
    public string deviceTypeName_ch;
    public int deviceID =1;
    public int inoutID = 1;

    private static List<DeviceSwitchgearSlider> allSlider = new List<DeviceSwitchgearSlider>();


    public GameObject normalStatusRotateObject;
    public float normalStatusRotateSpeed = 1.0f;
    void Awake()
    {
        allSlider.Add(this);
        if (showText!=null)
        {
            showText.text = deviceTypeName_ch + deviceID.ToString();
        }
    }
    
    void Update()
    {
            if (normalStatusRotateObject!= null&&switchgearSlider.value == 1)
            {
                normalStatusRotateObject.transform.Rotate(0,0,normalStatusRotateSpeed*Time.deltaTime);
            }

    }
    void OnDestroy()
    {
        allSlider.Remove(this);
    }
    public void SetDeviceSwitchgear(string name, int id, bool isopen)
    {
       

        if (name == deviceTypeName&&id == deviceID)
        {
            switchgearSlider.SetValue(isopen ? 1 : 0,false) ;
            
        }
    }
    public void SetDeviceSwitchgearValue(bool isopen)
    {

        switchgearSlider.SetValue(isopen ? 1 : 0, false);
    }
    public void OnSwitchgearValueChanded(float f)
    {
        Debug.Log("OnSwitchgearValueChanded:"+f);
        Panel_Homepage.OnDeviceSwitchgear(deviceTypeName, deviceID, f == 1 ? true : false);
    }
    
    public static void SetSliderValueByDeviceSwitchgear(string name,int id,bool isopen)
    {
        for (int i = 0; i < allSlider.Count; i++)
        {
            allSlider[i].SetDeviceSwitchgear(name,id,isopen);
        }
    }
    public static void SetSliderValueByInOutID(byte oh,byte ol)
    {

        for (int i = 0; i < allSlider.Count; i++)
        {

            if (allSlider[i].inoutID <=8)
            {
               
                allSlider[i].SetDeviceSwitchgearValue((ol & (0x1 << (allSlider[i].inoutID - 1)) ) > 0);
            }
            else if (allSlider[i].inoutID <= 16)
            {
                allSlider[i].SetDeviceSwitchgearValue((oh & (0x1 << (allSlider[i].inoutID - 9)))>0);
            }
            else
            {
                Debug.LogError("无效inoutid");
            }
        }
    }
    public void OnShowParameter()
    {
        PanelSwitcher.OpenDeviceParameterList();
        Panel_Homepage.OnShowDeviceParameter(deviceTypeName,deviceTypeName_ch,deviceID);
    }
   
}
