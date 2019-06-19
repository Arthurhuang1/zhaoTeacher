using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlObjectsRotate : MonoBehaviour {

    public Transform[] rotater;
    public Vector3 rotateEuler = Vector3.zero;
    public float rotateSpeed = 1.0f;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (rotater!=null)
        {
            for (int i = 0; i < rotater.Length; i++)
            {
                rotater[i].transform.Rotate(rotateEuler * rotateSpeed*Time.deltaTime);
            }
        }
		
	}
}
