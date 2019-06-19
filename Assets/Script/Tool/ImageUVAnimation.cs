using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class ImageUVAnimation : MonoBehaviour {

    Image image;
    Material uvAni;
    public float xMoveSpeed = 0;
    public float yMoveSpeed = 0;
    public bool isNewMaterialInstance = false;  
    void Start () {
        image =GetComponent<Image>();
        uvAni = image.material;
        if (isNewMaterialInstance)
        {
            image.material = new Material(uvAni);
            uvAni = image.material;
        }
    }
    Vector3 offset = Vector3.zero;
	void Update () {
       
        offset.x += xMoveSpeed * Time.deltaTime;
        offset.y  += yMoveSpeed * Time.deltaTime;
         image.material.SetTextureOffset("_MainTex", offset);


    }
    //rtsp://admin:poiu1234@192.168.1.184:554/Streaming/Channels/102?transportmode=multicast
    //rtsp://admin:poiu1234@192.168.1.184:8000/h265/ch1/main/av_stream
    //rtsp://admin:123456@192.168.1.192:554/h265/ch1/main/av_stream
    //rtsp://184.72.239.149/vod/mp4:BigBuckBunny_115k.mov
}
