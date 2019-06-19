using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class But_StarterListProcedureItem : MonoBehaviour, IPointerDownHandler{
    public string procedurename;
    public string procedurename_ch;

    bool isMouseDown = false;
    Vector2 v2MouseDownPos ;
    private RectTransform rTransform;
    private PointerEventData pointerData;
    public int childID = 0;
    public Text nameText;
    public GameObject switchgear;
    public GameObject waiting;

    public InputField waitingInputField;
    public Slider deviceOnOffSlider;
    public Dropdown deviceIdDropdown;

    public ProcedureType eProcedureType;
    public void SetNameTextcon(string con)
    {
        nameText.text = con;//秒
    }
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        isMouseDown = true;
        v2MouseDownPos = rTransform.position - Input.mousePosition;
    }

   

    private void Awake()
    {
        rTransform = GetComponent<RectTransform>();
        pointerData = new PointerEventData(EventSystem.current);
    }

    void Update () {
      
        if (isMouseDown)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("GetMouseButtonUp");
                isMouseDown = false;

           
                pointerData.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);
                bool isenterContentView = false;
                for (int i = 0; i < results.Count; i++)
                {
                    if (results[i].gameObject.tag == "ProcedureStarterContentView")
                    {
                        Debug.Log("Name:" + results[i].gameObject.name);
                       // ProcedureStarterContentView.OnDragEnterContentView();
                        isenterContentView = true;
                    }

                }
                if (isenterContentView == false) //拖到框外
                {
                    Debug.Log("isenterContentView: 拖到框外");
                    ProcedureStarterContentView.OnRemoveProcedureItem(this);
                }
                else//仍然在框内
                {
                    Debug.Log("isenterContentView: 仍然在框内");
                    ProcedureStarterContentView.RefreshChildPosition();
                }

            }
            else
            {
                rTransform.position = Input.mousePosition + new Vector3(v2MouseDownPos.x, v2MouseDownPos.y);
                ProcedureStarterContentView.OnMoveProcedureItem(this);
            }
        }
		
	}

    public static But_StarterListProcedureItem Create(But_ProcedureItem pi)
    {
        GameObject prefabs =Resources.Load<GameObject>("Prefabs/UI/But_StarterListProcedureItem");
        But_StarterListProcedureItem res = (Instantiate(prefabs)).GetComponent<But_StarterListProcedureItem>();
        res.eProcedureType = pi.eProcedureType;
        res.procedurename = pi.procedureName;
        if (pi.eProcedureType == ProcedureType.Switchgear)
        {
            res.switchgear.SetActive(true);
            
        }
        if (pi.eProcedureType == ProcedureType.Waiting)
        {
            res.waiting.SetActive(true);
        }
        return res;
    }


    public string GetProcedureName()
    {
        return procedurename;
    }
    public string GetProcedureName_ch()
    {
        return procedurename_ch;
    }
    public float GetWaitingSecond()
    {
        return float.Parse(waitingInputField.text);
    }
    public int GetSwitchgearId()
    {
        return deviceIdDropdown.value +1;
    }
    public bool GetDeviceSliderValue()
    {
        return deviceOnOffSlider.value == 1 ? true : false;
    }
}
