using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class LerpImageColor : MonoBehaviour {


    public Color startColor;
    public Color EndColor;
    private Color originalColor;
    private Image image;
    public float speed = 1;
    private void Awake()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
    }
    public bool isEnable = true;
    void Update ()
    {
        if (isEnable&&image != null)
        {
            image.color = Color.Lerp(startColor, EndColor,(Mathf.Cos(Time.time* speed) +1.0f)/2.0f);
        }
	}
    public void Open()
    {
        isEnable = true;
    }
    public void Close(bool isRecoverColor = true)
    {
        isEnable = false;
        if (isRecoverColor)
        {
            image.color = originalColor;
        }
    }

}
