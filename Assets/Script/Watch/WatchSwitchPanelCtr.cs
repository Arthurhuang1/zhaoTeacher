using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchSwitchPanelCtr : MonoBehaviour {

	void Start () {
		
	}
    Vector2 touchPhaseBeganPosition;
    public float switchPaneloffsetx = 150.0f;
    public GameObject[] panels;
    void Update ()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchPhaseBeganPosition = Input.GetTouch(0).position;

            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Vector2 offset = Input.GetTouch(0).position - touchPhaseBeganPosition;
                Vector2 usoffset;
                usoffset.x = offset.x >= 0? offset.x: - offset.x;
                usoffset.y = offset.y >= 0? offset.y: - offset.y;
              
                if (usoffset.x > usoffset.y)
                {
                    if (usoffset.x>= switchPaneloffsetx)
                    {
                        SwitchPanel();
                    }
                }

            }

        }
       


    }
    void SwitchPanel()
    {
        if (panels!=null)
        {
            if (panels[0].gameObject.activeSelf)
            {
                panels[0].gameObject.SetActive(false);
                panels[1].gameObject.SetActive(true);
            }
            else
            {
                panels[0].gameObject.SetActive(true);
                panels[1].gameObject.SetActive(false);
            }
        }
    }
}
