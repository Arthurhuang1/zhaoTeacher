using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public struct DeviceParameter
{
	public enum ValueType
	{
		SwitchingQuantity = 0,
		AnalogQuantity =1,
	}
	public string deviceName;
	public string parameterName;

	public ValueType valueType;
	public float value;

	public DeviceParameter(string deviceName,string parameterName,float value,ValueType valueType =  ValueType.SwitchingQuantity)
	{
		this.deviceName = deviceName;
		this.parameterName = parameterName;
		this.value = value;
		this.valueType = valueType;
	}

}
public class DeviceParameterShow : MonoBehaviour {


	public Text paratext;
	public Image analogFillImage;
	public Image switchingImage;
	void Start () {
		
	}
	
	
	void Update () {
		
	}
	public void Show(DeviceParameter dp)
	{
		paratext.text = dp.parameterName;
		analogFillImage.transform.localScale = new  Vector3(dp.value,1,1);
		if(dp.value >0.5f)
		{
			switchingImage.color = new Color(0.0f,252/(float)255,1.0f,1.0f);
			analogFillImage.color = new Color(54/(float)255,146/(float)255,195/(float)255,1.0f);
		}
		else
		{
			switchingImage.color = new Color(1,0,0,1);
			analogFillImage.color = new Color(1,0,0,1);
		}
		switch (dp.valueType)
		{
			case DeviceParameter.ValueType.SwitchingQuantity:
			break;
			case DeviceParameter.ValueType.AnalogQuantity:
			break;
		}
	}
}
