using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProcedureStarterEditor : ControllerBase
{
    public ProcedureStarter target;
    public ProcedureStarterContentView ProcedureStarterContentView;
    private static EventSystem currEventSystem;
    private static PointerEventData pointerData;
    private void Awake()
    {
        currEventSystem = EventSystem.current;
         pointerData = new PointerEventData(currEventSystem);
    }
    void Update()
    {
        if (isSelectProcedure)
        {
            pointerData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            bool isenterContentView = false;
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.tag == "ProcedureStarterContentView")
                {
                    ProcedureStarterContentView.OnDragEnterContentView();
                    isenterContentView = true;
                }

            }
            if (!isenterContentView && ProcedureStarterContentView.IsDragEnterContentView)
            {
                ProcedureStarterContentView.OnDragExitContentView();
            }
        }
        
    }


    public ProcedureStarterEditor(ProcedureStarter ps)
    {
        target = ps;

    }
    private static bool isSelectProcedure = false;
    private static But_ProcedureItem curSelectedProcedureItem;
    public static void OnSelectProcedure(But_ProcedureItem but)
    {
        isSelectProcedure = true;
        curSelectedProcedureItem = but;
        Image_DragSelectedProcedure.EnableDrag(but);
    }
    public static void OnDropDragImage()
    {

        isSelectProcedure = false;
        pointerData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject.tag == "ProcedureStarterContentView")
            {
                ProcedureStarterContentView.OnAddProcedureItem(curSelectedProcedureItem);
            }

        }

    }

   


}