using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcedureStarterContentView : MonoBehaviour {

    public static ProcedureStarterContentView Insatnce { get; private set; }
    private static Dictionary<int, But_StarterListProcedureItem> starterList = new Dictionary<int, But_StarterListProcedureItem>();
    public static Dictionary<int, But_StarterListProcedureItem> StarterList { get { return starterList; } private set { } }
    private static RectTransform rTransform;
    private static RectTransform content;
    private static RectTransform pendingProcedureItemImage;
    public float fProcedureItemImageHeight = 0;
    public float fProcedureItemImageOverlapHeight = 0;
    public float fContentViewReserveLenght = 200;



    private void Awake()
    {
        Insatnce = this;
        rTransform = GetComponent<RectTransform>();
        content = rTransform.Find("Content").GetComponent<RectTransform>();
        pendingProcedureItemImage = rTransform.Find("Content_pos/PendingProcedureItemImage").GetComponent<RectTransform>();
        IsDragEnterContentView = false;

        starterList.Clear();
        for (int i = 0; i < content.childCount; i++)
        {
            But_StarterListProcedureItem proitem = content.GetChild(i).GetComponent<But_StarterListProcedureItem>();
            starterList.Add(proitem.childID, proitem);
        }
    }
    public static bool IsDragEnterContentView { get; private set; }
    private static int PendingReplaceId = 0;
    public static void OnDragEnterContentView()
    {
        IsDragEnterContentView = true;
       Vector2 pos = content.position - Input.mousePosition   ;
      
        float y = pos.y / (Insatnce.fProcedureItemImageHeight - Insatnce.fProcedureItemImageOverlapHeight) ;

        int ny = (int)y +1;//替换ny个
        PendingReplaceId = ny;

        if (ny <= content.childCount+1)
        {
            Vector3 localPosition;
            foreach (var item in starterList)
            {
                localPosition = item.Value.transform.localPosition;
                localPosition.x = content.sizeDelta.x / 2.0f;

                if (item.Key >= ny)
                {
                    localPosition.y = (item.Key-1+1) * -116;//+1-1好理解 

                }
                else
                {
                    localPosition.y = (item.Key-1) * -116;
                }
                item.Value.transform.localPosition = localPosition;
            }
            // pendingProcedureItemImage.transform.position= content.position +new Vector3(content.sizeDelta.x/2.0f, (ny -1) * -116, 0);
            pendingProcedureItemImage.transform.localPosition= new Vector3(content.sizeDelta.x/2.0f, (ny -1) * -116, 0);
        }
        else //todo 放置在最后一个
        {
            PendingReplaceId = content.childCount + 1;
        }
        
    }

    public static void OnDragExitContentView()
    {
        IsDragEnterContentView = false;

        RefreshChildPosition();
    }

    internal static void OnMoveProcedureItem(But_StarterListProcedureItem child)
    {
        
    }
    internal static void OnRemoveProcedureItem(But_StarterListProcedureItem child)
    {
    
        for (int i = 0; i < content.childCount ; i++)
        {
            if (i< child.childID)
            {
                continue;
            }
            else
            {
                starterList[i] = starterList[i + 1];
                starterList[i].childID = i;
                Debug.Log("OnRemoveProcedureItem i:" +i);
            }
        }
        starterList.Remove(content.childCount);
        Debug.Log("starterList.Remove :" + content.childCount);
        Debug.Log("OnRemoveProcedureItem id:" + child.childID);
        Destroy(child.gameObject);
        RefreshChildPosition();
    }
    internal static void OnAddProcedureItem(But_ProcedureItem but)
    {
        //Debug.Log("Add:"+ PendingReplaceId +"name:"+ but.procedureName_ch);

        But_StarterListProcedureItem newitem = But_StarterListProcedureItem.Create(but);
        newitem.SetNameTextcon(but.procedureName_ch);

        if (PendingReplaceId <= content.childCount) //中间
        {
           
            starterList.Add(content.childCount + 1, starterList[content.childCount]);
            starterList[content.childCount + 1].childID = content.childCount + 1;
            for (int i = content.childCount; i >= 1; i--)
            {
                if (i > PendingReplaceId)
                {
                    starterList[i] = starterList[i - 1];
                    starterList[i].childID = i;
                }
            }
            starterList[PendingReplaceId] = newitem;


        }
        else//最后一个
        {
           // Debug.Log("添加到最后:"+ PendingReplaceId);
            starterList.Add(PendingReplaceId, newitem);
        }


        newitem.transform.SetParent(content);
        newitem.childID = PendingReplaceId;
        RefreshChildPosition();

    }



    internal static void RefreshChildPosition()
    {
        Vector3 localPosition;
        foreach (var item in starterList)
        {
            localPosition = item.Value.transform.localPosition;
            localPosition.x= content.sizeDelta.x / 2.0f;
            localPosition.y = (item.Key-1 )* -116;
            item.Value.transform.localPosition = localPosition;


        }
        pendingProcedureItemImage.transform.position = content.position + new Vector3(content.sizeDelta.x / 2.0f, 1000, 0);
        content.sizeDelta =new Vector2(content.sizeDelta.x,(Insatnce.fProcedureItemImageHeight - Insatnce.fProcedureItemImageOverlapHeight) * starterList.Count + Insatnce.fContentViewReserveLenght);
    }
}
