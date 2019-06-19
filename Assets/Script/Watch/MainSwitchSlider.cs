using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSwitchSlider : MonoBehaviour {

	public Transform sliderButtonList ;
    private Slider[] ctrSliders;
    private void Awake()
    {
        ctrSliders = sliderButtonList.GetComponentsInChildren<Slider>();
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnMainSwitchVluaeChange(float v)
    {
        if (ctrSliders!=null)
        {
            for (int i = 0; i < ctrSliders.Length; i++)
            {
                ctrSliders[i].interactable = v == 1 ? true : false;
            }
        }
    }
}
