using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Blackcover : MonoBehaviour {

    private float a = 0;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	    if(Time.time < 5)
        {
            GetComponent<Image>().color = new Color(0, 0, 0, a); 
            a += 0.001f;
        }
	}
}
