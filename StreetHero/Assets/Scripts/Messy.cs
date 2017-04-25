using UnityEngine;
using System.Collections;

public class Messy : MonoBehaviour {

    public GameObject man01;
    public GameObject man02;
    private float lastTime = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	void Update () {
	   if(Time.time - lastTime > 8)
        {
            lastTime = Time.time;
            Instantiate(man01, new Vector3(-28f, -1.19f, 0), Quaternion.identity);
            Instantiate(man02, new Vector3(39.2f, -1.19f, 0), Quaternion.identity);
        }
       
	}
}
