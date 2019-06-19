using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour {

    public Transform zero;
    public Rect renderRect;
	void Start () {
        renderRect = GetComponent<Canvas>().pixelRect;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
