using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sky : MonoBehaviour {

    private Image srcImg;
    public Sprite[] skyImg;
    private float t = 0;
    private int k = 0;
	// Use this for initialization
	void Start () {
        srcImg = GetComponent<Image>();
        srcImg.sprite = skyImg[0];
	}
	
	// Update is called once per frame
	void Update () {
	   if(Time.time - t >= 5f)
        {
            t = Time.time;
            srcImg.sprite = skyImg[k];
            k++;
        }
	}
}
