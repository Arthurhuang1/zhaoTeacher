using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolRobot : MonoBehaviour {


    public Transform startingPoint;
    public Transform endPoint;
    public Animator ani;
    void Start()
    {


    }
    public float currpProgress = 0;
    public float moveSpeed = 1.0f;
    public bool isFinishEnd = false;
    void Update()
    {
        if (ani != null)
        {
            ani.SetFloat("moveSpeed", moveSpeed);
        }
        if (!isFinishEnd)
        {
            currpProgress += moveSpeed * Time.deltaTime;
            if (currpProgress >= 1)
            {
                currpProgress = 1;
                isFinishEnd = true;
                if (ani!=null)
                {
                    ani.transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                }
            }
        }
        else
        {
            currpProgress -= moveSpeed * Time.deltaTime;
            if (currpProgress <= 0)
            {
                currpProgress = 0;
                isFinishEnd = false;
                if (ani != null)
                {
                    ani.transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                }
            }
        }
        transform.position = Vector3.Lerp(startingPoint.position, endPoint.position, currpProgress);
    }
}
