using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class But_ProcedureItem : MonoBehaviour,IPointerDownHandler {


    public ProcedureType eProcedureType = ProcedureType.Switchgear;
    public string procedureName;
    public string procedureName_ch;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
       // Debug.Log("OnPointerDown");
        ProcedureStarterEditor.OnSelectProcedure(this);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
//等待