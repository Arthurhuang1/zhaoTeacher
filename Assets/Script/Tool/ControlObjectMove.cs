using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlObjectMove : MonoBehaviour {
    public Transform ctrObject;
    public Transform start;
    public Transform end;
    public float moveSpeed = 1.0f;
    private float curPro = 0;
    private bool isToEndpoint = false;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!isToEndpoint)
        {
            curPro += moveSpeed * Time.deltaTime;
            if (curPro>=1.0f)
            {
                curPro = 1.0f;
                isToEndpoint = true;
            }
        }
        else
        {
            curPro -= moveSpeed * Time.deltaTime;
            if (curPro <=0.0f)
            {
                curPro = 0.0f;
                isToEndpoint = false;
            }
        }

        ctrObject.position = Vector3.Lerp(start.position,end.position,curPro);

        //383838FF   异常参数

    }
}
