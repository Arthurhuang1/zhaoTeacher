using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitcher : MonoBehaviour {

    public static PanelSwitcher Instance { get; private set; }
    public GameObject[] Panels;
    private void Awake()
    {
        Instance = this;
    }
    public static Action<string> onOpenPanel;
    public static Action<string> onClosePanel;
    private static string currentOpenPanel = "Panel_Homepage";
    public static void OpenPanel(string openname)
    {
        ClosePanel();
        currentOpenPanel = openname;
        for (int i = 0; i < Instance.Panels.Length; i++)
        {
            if (Instance.Panels[i].name == openname)
            {
                Instance.Panels[i].SetActive(true);
            }
            else
            {
                Instance.Panels[i].SetActive(false);
            }
        }
        if (onOpenPanel!=null)
        {
            onOpenPanel(openname);
        }
    }
    public static void ClosePanel()
    {
        if (onClosePanel!=null)
        {
            onClosePanel(currentOpenPanel);
        }
    }
    public static void OpenSetting()
    {
        OpenPanel("Panel_Seting");

    }
    public static void ReturnHomepage()
    {
        OpenPanel("Panel_Homepage");

    }
    public static void OpenDeviceParameterList()
    {
        OpenPanel("Panel_DeviceParameterList");
    }
   
}
