using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotateWatchBG : MonoBehaviour {



    float rotateSpeed = 1f;
    bool isStartSlide = false;
    float averageDeltaPositions = 0;
    int deltaPositionsindex = 0;
    bool isRecordFramesMax = false;
    static int recordFrames = 500;
    float[] deltaPositions = new float[recordFrames];
    float slideTime = 0.0f;
    float slideTimeParame_k = 0.050f;
    float slideTimeParame_b = -4;
    public static float rotater = 0;
    private static float rotatetotal = 0;
    private static float maxRotatetotal = 5000;
    private static float minRotatetotal = 0;
    private static void SetRotatetotalIncrement(float add)
    {
        rotatetotal += add;
        rotatetotal = Mathf.Clamp(rotatetotal, minRotatetotal, maxRotatetotal);
        //Debug.Log(rotatetotal);
    }
    public static float GetRotatetotal_minmax( )
    {
        return rotatetotal;
    }
    public static float GetRotatetotal_01()
    {
        return rotatetotal/(maxRotatetotal - minRotatetotal);
    }
    void Update () {
        // if (Input.GetMouseButton(0))
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            deltaPositionsindex = 0;
            isRecordFramesMax = false;
            isStartSlide = false;

        }
        if (Input .touchCount == 1&& Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            rotater = -Input.GetTouch(0).deltaPosition.y * rotateSpeed;
            SetRotatetotalIncrement(rotater);
            transform.Rotate(0, 0, rotater);
            //Debug.Log(Input.GetTouch(0).deltaPosition.y);
            deltaPositions[deltaPositionsindex] = Input.GetTouch(0).deltaPosition.y;
            deltaPositionsindex++;
            if (deltaPositionsindex>= recordFrames)
            {
                deltaPositionsindex = 0;
                isRecordFramesMax = true;
            }

        }
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            isStartSlide = true;
            float sum = 0;
            if (isRecordFramesMax)
            {
                deltaPositionsindex = recordFrames;
            }
            for (int i = 0; i < deltaPositionsindex; i++)
            {
                sum += deltaPositions[i];
            }
            averageDeltaPositions = sum / (float)deltaPositionsindex;
          
            float tem_averageDeltaPositions = averageDeltaPositions > 0 ? averageDeltaPositions : -averageDeltaPositions;
            tem_averageDeltaPositions += slideTimeParame_b;

            slideTime = tem_averageDeltaPositions <= 0 ? 0 : tem_averageDeltaPositions*slideTimeParame_k;

        }
        if (isStartSlide)
        {
            if (slideTime <= 0)
            {
                isStartSlide = false;
            }
            else
            {
                rotater = -averageDeltaPositions * rotateSpeed;
               
                if (float.IsNaN(rotater))
                {
                    Debug.Log("rotater == double.NaN");
                    rotater = 0;
                }
                SetRotatetotalIncrement(rotater);

                transform.Rotate(0, 0, rotater);


                slideTime -= Time.deltaTime;
                averageDeltaPositions = Mathf.Lerp(averageDeltaPositions,0,2.5f*Time.deltaTime);
                if (float.IsNaN(averageDeltaPositions))
                {
                    averageDeltaPositions = 0;
                }


            }
        }
    }
}
