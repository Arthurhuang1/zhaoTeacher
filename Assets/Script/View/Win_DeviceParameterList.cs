using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win_DeviceParameterList : MonoBehaviour 
{
	public Transform ScrollContent_Parameter;
	public Transform ScrollContent_ErrorParameter;

	public Text deviceNameText;
	public float contentItemHeight = 40.0f;

    public void Open(string deviceName,List<DeviceParameter> paras,List<DeviceParameter>  errparas = null)
	{
		Close();
		gameObject.SetActive(true);

		deviceNameText.text = deviceName;
		
		if(paras!=null)
		{
			for (int i = 0; i < paras.Count; i++)
			{
				if (i< ScrollContent_Parameter.childCount)
				{
					ScrollContent_Parameter.GetChild(i).gameObject.SetActive(true);
					ScrollContent_Parameter.GetChild(i).GetComponent<DeviceParameterShow>().Show(paras[i]);
				}
				else	
				{
					//todo 做对象池  
					Debug.LogError("内容按钮不够");
				}
			}
			 
			(ScrollContent_Parameter as RectTransform).sizeDelta = new 	Vector2(184,contentItemHeight*paras.Count);
		}
		if(errparas!=null)
		{
			for (int i = 0; i < errparas.Count; i++)
			{
				if (i < ScrollContent_ErrorParameter.childCount)
				{
					ScrollContent_ErrorParameter.GetChild(i).gameObject.SetActive(true);
					ScrollContent_ErrorParameter.GetChild(i).GetComponent<DeviceParameterShow>().Show(errparas[i]);
				}
				else	
				{
					//todo 做对象池
					Debug.LogError("内容按钮不够");
				}
			}
			(ScrollContent_ErrorParameter as RectTransform).sizeDelta = new 	Vector2(184,30*errparas.Count);
		}
		
	}
public	void Close()
	{
		HideAllContent();
		gameObject.SetActive(false);
	}
	
	

	void HideAllContent()
	{
		for (int i = 0; i < ScrollContent_Parameter.childCount; i++)
		{
			ScrollContent_Parameter.GetChild(i).gameObject.SetActive(false);
		}
		for (int i = 0; i < ScrollContent_ErrorParameter.childCount; i++)
		{
			ScrollContent_ErrorParameter.GetChild(i).gameObject.SetActive(false);
		}
	}
}
