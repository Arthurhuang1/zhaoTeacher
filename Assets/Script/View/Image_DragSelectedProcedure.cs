using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Image_DragSelectedProcedure : MonoBehaviour,IPointerUpHandler {

    public static Image_DragSelectedProcedure Instance
    { get; private set; }

    RectTransform rtransform;
    private void Awake()
    {
        Instance = this;
        rtransform = GetComponent<RectTransform>();
        DisableDrag();
    }
    private void Update()
    {
       rtransform.position = Input.mousePosition;
  

        
        if (Input.GetMouseButtonUp(0))
        {

            DisableDrag();
            ProcedureStarterEditor.OnDropDragImage();
        }

    }
    public static void EnableDrag(But_ProcedureItem but)
    {
        
        Instance.gameObject.SetActive(true);
       Instance.GetComponent<Image>().sprite = but.GetComponent<Image>().sprite;
    }
    public static void DisableDrag()
    {
        Instance.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
