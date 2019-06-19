using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Seting : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnClickReturnHomepage()
    {
        GameMode.StartupProcess = ProcedureStarter.GenerateProcedureStarterByStarterList(ProcedureStarterContentView.StarterList);

        PanelSwitcher.ReturnHomepage();
    }
}
