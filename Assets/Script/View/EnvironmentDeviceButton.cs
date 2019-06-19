using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentDeviceButton : MonoBehaviour {

	public string deviceTypeName;
	public string deviceTypeName_ch;
	public int deviceID;
    public GameObject normalStatusRotateObject;
    public float normalStatusRotateSpeed = 1.0f;
    void Start () 
	{
		
	}
	
	
	void Update () {
        if (normalStatusRotateObject!=null)
        {
            normalStatusRotateObject.transform.Rotate(0, 0, normalStatusRotateSpeed * Time.deltaTime);
        }
		
	}

	public void OnShowParameter()
    {
        PanelSwitcher.OpenDeviceParameterList();
        Panel_Homepage.OnShowDeviceParameter(deviceTypeName,deviceTypeName_ch,deviceID);
    }
}
//环境温度 Temperature 
//烟雾 Smoke
//瓦斯浓度  Mashgas
//一氧化碳浓度 CarbonMonoxide