using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderButtonList : MonoBehaviour {

    float  currShowBottom =0;
    private int showButtonCount = 5;
    public BezierInstance bezierInstance;
    void Start () {
        InOut16.instance.onUpdateIn16Out16 = OnUpdateIn16Out16;
    }
    void OnUpdateIn16Out16(byte ih,byte il,byte oh,byte ol)
    {
        DeviceSwitchgearSlider.SetSliderValueByInOutID(oh, ol);

    }
    public bool isBezierLerp = true;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            IncreaseCurrentShowBottom(0.1f);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            IncreaseCurrentShowBottom(-0.1f);
        }
        UpdateCurrentShowBottomByRotate();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i >= GetCurrentShowBottom() && i < GetCurrentShowBottom() + showButtonCount)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                if (isBezierLerp)
                {
                    transform.GetChild(i).position = bezierInstance.GetBezierLerpPoint(i - GetCurrentShowBottom() == 0 ? 0 : (i - GetCurrentShowBottom()) / showButtonCount);

                }
                else
                {
                    transform.GetChild(i).position = bezierInstance.GetLineLerpPoint(i - GetCurrentShowBottom() == 0 ? 0 : (i - GetCurrentShowBottom()) / showButtonCount);

                }
                transform.GetChild(i).localScale = GetScaleByY(transform.GetChild(i).position.y);
              //  transform.GetChild(i).localScale = Vector3.one*0.5f;
            }
            else
            {
                transform.GetChild(i).position = new Vector3(0, 1000, 0);
                transform.GetChild(i).localScale = Vector3.zero;
                transform.GetChild(i).gameObject.SetActive(false);
            }

        }
    }
    Vector3 GetScaleByY(float y)
    {
        float f = (transform.position.y >= y ? transform.position.y - y :y - transform.position.y);
        return Vector3.one * Mathf.Lerp(1,0,(f/300));
    }
    void UpdateCurrentShowBottomByRotate()
    {
        currShowBottom = AutoRotateWatchBG.GetRotatetotal_01()*(transform.childCount)-2.5f;
        if (currShowBottom <= -2.5f)
        {
            currShowBottom = -2.5f;
        }
        if (currShowBottom + 2.5f >= transform.childCount)
        {
            currShowBottom = transform.childCount - 2.5f;
        }
    }
    float GetCurrentShowBottom()
    {
        if (currShowBottom <= -2.5f)
        {
            currShowBottom = -2.5f;
        }
        if (currShowBottom + 2.5f >= transform.childCount)
        {
            currShowBottom = transform.childCount - 2.5f;
        }
        return currShowBottom;
    }
    float IncreaseCurrentShowBottom(float f )
    {
        currShowBottom += f;

        
        if (currShowBottom <= -2.5f)
        {
            currShowBottom = -2.5f;
        }
        if (currShowBottom + 2.5f >= transform.childCount)
        {
            currShowBottom = transform.childCount - 2.5f;
        }

        return currShowBottom;

    }
}
