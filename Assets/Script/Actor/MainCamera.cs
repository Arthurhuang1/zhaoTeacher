using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Camera))]
public class MainCamera : MonoBehaviour {

    public bool isDraw = true;
    public Transform curveDrawPointRectStart;
    public Transform curveDrawPointRectEnd;
    public Text showCurveValueText;
    
    public Image showCurveValueImage;
    private Rect curveDrawRect;
    private Vector3 curveDrawPoint_start;
    private Vector3 curveDrawPoint_end;
    private AnimationCurve curveValue;
    private Camera mainCamera;
    Keyframe[] curvKeys;
    public Material GLMat;

    void Start ()
    {
        mainCamera = GetComponent<Camera>();
        PanelSwitcher.onClosePanel += OnClosePanel;
        PanelSwitcher.onOpenPanel += OnOpenPanel;

        curveDrawPoint_start = mainCamera.WorldToViewportPoint(curveDrawPointRectStart.position);
        curveDrawPoint_end = mainCamera.WorldToViewportPoint(curveDrawPointRectEnd.position);
        curveDrawRect = new Rect(curveDrawPoint_start.x, curveDrawPoint_start.y, (curveDrawPoint_end - curveDrawPoint_start).x,(curveDrawPoint_end - curveDrawPoint_start).y);
        curvKeys = new Keyframe[20];
        for (int i = 1; i < curvKeys.Length; i++)
        {
            curvKeys[i].time = (i) / (float)curvKeys.Length;
            curvKeys[i].value = Random.Range(0.4f, 0.7f);
        }
        curveValue = new AnimationCurve();
        curveValue.keys = curvKeys;
    }
    float moveTime = 0;
    public float movetimeSpeed = 1.0f;

	void Update () {
        moveTime += Time.deltaTime* movetimeSpeed;
        if (moveTime>=1.0f)
        {
            moveTime = 0.0f;
            for (int i =  curvKeys.Length-1; i >0; i--)
            {
                curvKeys[i].value = curvKeys[i-1].value;
            }
            curvKeys[0].value += Random.Range(-0.2f, 0.2f);
            curvKeys[0].value = Mathf.Clamp(curvKeys[0].value, 0.2f,0.8f);

        }
        for (int i = 0; i < curvKeys.Length; i++)
        {
            curvKeys[i].time = i /(float)curvKeys.Length + moveTime / (float)curvKeys.Length;
        }
        curveValue.keys = curvKeys;
       
    }
    float targetvalue = 0;
    public int pixCount = 1000;
    public Color topColor;
    public Color downColor;
    private void OnPostRender()
    {
        if (isDraw&&isOpenPanel_Homepage)
        {
            OnDrawPanel_Homepage();
        }
    }
    private float curveValueShowRange = 0.8f;
    public  float GetCurveValue(float f)
    {

        return curveValue.Evaluate(f * curveValueShowRange + (1.0f - curveValueShowRange) / 2.0f);
    }
    private float GetCurveRectPos_x(float x)
    {
       return curveDrawRect.width*x+curveDrawPoint_start.x;
    }
    private float GetCurveRectPos_y(float y)
    {
        return curveDrawRect.height * y + curveDrawPoint_start.y;
    }

    void RandomCurve()
    {

        Keyframe k = curveValue.keys[0];
        targetvalue += Random.Range(-0.01f, 0.01f);
        targetvalue = Mathf.Clamp(targetvalue, 0.1f, 0.9f);
        k.value = Mathf.Lerp(k.value, targetvalue, Time.deltaTime);
        Keyframe oldk = curveValue.keys[0];
        curveValue.MoveKey(0, k);

        Keyframe newk;
        for (int i = 1; i < curveValue.length; i++)
        {
            newk = curveValue.keys[i];
            newk.value = Mathf.Lerp(newk.value, oldk.value, Time.deltaTime);
            oldk = curveValue.keys[i];
            curveValue.MoveKey(i, newk);
        }
    }

    private bool isOpenPanel_Homepage = true;
    void OnOpenPanel(string panelName)
    {
        if (panelName == "Panel_Homepage")
        {
            isOpenPanel_Homepage = true;
        }

    }
    void OnClosePanel(string panelName)
    {
        if (panelName == "Panel_Homepage")
        {
            isOpenPanel_Homepage = false;
        }
    }
    public bool isShowHorizonal = true;
    public bool isShowVertical = true;
    public bool isShowVerticalR = true;
    public bool isShowVerticalL = true;
    void OnDrawPanel_Homepage()
    {
        if (GLMat!=null)
        {
            GLMat.SetPass(0);
        }
        //GL.Clear(true, true, Color.black);
        GL.PushMatrix();
        GL.LoadOrtho();
        if (showCurveValueText!=null)
        {
            showCurveValueText.text = GetCurveValue(0).ToString("0.00");
        }
        if (showCurveValueImage!=null)
        {
            showCurveValueImage.transform.position = curveDrawPointRectStart.position + new Vector3(0, (curveDrawPointRectEnd .position.y- curveDrawPointRectStart.position.y)* GetCurveValue(0), 0);
        }
        Color topColora0 = topColor;
        topColora0.a = 0.0f;
        Vector3 lastpos = Vector3.zero;
        for (int i = 0; i < pixCount; i++)
        {
            float idp = i / (float)pixCount;
            float x = GetCurveRectPos_x(idp);
            float y = GetCurveRectPos_y(GetCurveValue(idp));
           if (isShowHorizonal&&i > 1)
            {
                GL.Begin(GL.LINE_STRIP);
                if (idp <= 0.005f)
                {
                    GL.Color(topColora0);
                }
                else
                {
                  //  GL.Color(topColor);
                    Color c = Color.green;
                    c.a = y;
                    GL.Color(c);
                }
                GL.Vertex3(lastpos.x, lastpos.y,0);
                GL.Vertex3(x, y, 0);
                GL.End();
            }

           if (isShowVertical)
            {
                GL.Begin(GL.LINE_STRIP);
                GL.Color(topColor);
                GL.Vertex3(x, y, 0);
                GL.Color(topColor);
                GL.Vertex3(x, GetCurveRectPos_y(0.0f), 0);
                GL.End();
            }
            if (i > 1)
            {
                if (isShowVerticalL)
                {
                    GL.Begin(GL.LINE_STRIP);
                    GL.Color(topColor);
                    GL.Vertex3(lastpos.x, lastpos.y, 0);
                    GL.Color(downColor);
                    GL.Vertex3(x, GetCurveRectPos_y(0.0f), 0);
                    GL.End();
                }
               if (isShowVerticalR)
                {
                    GL.Begin(GL.LINE_STRIP);
                    GL.Color(topColor);
                    GL.Vertex3(x, y, 0);
                    GL.Color(downColor);
                    GL.Vertex3(lastpos.x, GetCurveRectPos_y(0.0f), 0);
                    GL.End();
                }
            }
            lastpos = new Vector3(x, y, 0);
        }
      
        GL.PopMatrix();


      
    }
}
//rtsp://admin:poiu1234@192.168.1.184:554/Streaming/Channels/102?transportmode=multicast