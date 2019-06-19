using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CloseDeviceParameterList : MonoBehaviour,IPointerClickHandler {

	public GameObject closedObject;
	public void OnPointerClick(PointerEventData eventData)
	{

		closedObject.SetActive(false);
        PanelSwitcher.ReturnHomepage();

    }
}
