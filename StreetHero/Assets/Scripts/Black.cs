using UnityEngine;
using System.Collections;

public class Black : MonoBehaviour {

    public bool explode = false;
    public bool changeLayer = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (changeLayer)
        {
            transform.parent.gameObject.GetComponent<Canvas>().sortingLayerName = "EndingBG";
        }
	}
}
