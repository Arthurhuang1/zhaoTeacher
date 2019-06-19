#define DEBUG_BEZIER
#undef DEBUG_BEZIER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BezierInstance : MonoBehaviour {

    public bool isUpdate = false;
    public bool isInstaceObj = false;
    public bool isActiveObj = false;
    public Transform startpoint, centrepoint, endpoint;
    public int granularity = 100;
    private Vector3[] bezierpoints;
    private GameObject[] bezierobjs;
#if DEBUG_BEZIER
    GameObject no;
#endif
    private void Awake()
    {
        BezieInit();
    }
    void Start () {

#if DEBUG_BEZIER
        no =  Instantiate(startpoint.gameObject, GetBezierLerpPoint(0.995f), Quaternion.identity) as GameObject;
        no.transform.SetParent(this.transform);
        no.GetComponent<Image>().color = Color.cyan;
#endif
    }


    void Update () {
        if (isUpdate)
        {
            CalculationPoint();
        }
#if DEBUG_BEZIER
        no.transform.position = GetBezierLerpPoint(AutoRotateWatchBG.GetRotatetotal_01());
#endif

    }
    public Vector3 GetBezierPoint(int n)
    {
        return bezierpoints[n];
    }
    public Vector3 GetBezierLerpPoint(float f)
    {
        Vector3 res = Vector3.zero;

        if (granularity == 0)
        {
            return res;
        }
        if (f <= 0)
        {
            return startpoint.position;
        }
        if (f >= 1)
        {
            return endpoint.position;
        }

        float x = granularity * f;

        //Debug.Log(x);
        Vector3 start = bezierpoints[(int)x];
        Vector3 end;
        if (x+1 >= bezierpoints.Length)
        {
            end = endpoint.position;
        }
        else
        {
            end = bezierpoints[(int)x + 1];
        }

   

        float t1 = ((int)x+1)/(float)granularity - ((int)x)/(float)granularity;
        float t2 = f - ((int)x)/(float)granularity;
        res = Vector3.Lerp(start, end, t2/t1);
      
      
        return res;
    }
    public Vector3 GetLineLerpPoint(float f)
    {
        return Vector3.Lerp(startpoint.position, endpoint.position, f);
    }
    public void BezieInit()
    {
        if (granularity == 0)
        {
            return;
        }

        Vector3 a2b, b2c;
        bezierpoints = new Vector3[granularity];
        if (isInstaceObj) bezierobjs = new GameObject[granularity];

        for (int i = 0; i < granularity; i++)
        {
            a2b = Vector3.Lerp(startpoint.position, centrepoint.position, i / (float)(granularity));
            b2c = Vector3.Lerp(centrepoint.position, endpoint.position, i / (float)(granularity));
            bezierpoints[i] = Vector3.Lerp(a2b, b2c, i / (float)(granularity));
            if (isInstaceObj)
            {
                bezierobjs[i] = Instantiate(startpoint.gameObject, bezierpoints[i], Quaternion.identity) as GameObject;
                bezierobjs[i].transform.SetParent(this.transform);
                bezierobjs[i].name = i.ToString();
                if (isActiveObj)
                {
                    bezierobjs[i].SetActive(isActiveObj);
                }
              
            }
          
        }
    }
    public  void CalculationPoint()
    {
        if (granularity == 0)
        {
            return;
        }

        Vector3 a2b, b2c;
     
        for (int i = 0; i < granularity; i++)
        {
            a2b = Vector3.Lerp(startpoint.position, centrepoint.position, i / (float)granularity);
            b2c = Vector3.Lerp(centrepoint.position, endpoint.position, i / (float)granularity);
            bezierpoints[i] = Vector3.Lerp(a2b, b2c, i / (float)granularity);
            if (isInstaceObj&& isActiveObj)
            {
                bezierobjs[i].transform.position = bezierpoints[i];
                bezierobjs[i].SetActive(isActiveObj);
            }
               
        }
    }
}
