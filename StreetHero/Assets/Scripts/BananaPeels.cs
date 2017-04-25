using UnityEngine;
using System.Collections;

public class BananaPeels : MonoBehaviour {

    // Use this for initialization
    private float startTime;
	void Start () {
        startTime = Time.time;
    }
    
	
	// Update is called once per frame
	void Update () {
	
       
        
        if (Time.time - startTime > 7f)
        {
            Destroy(gameObject);
        }
        else if (Time.time - startTime > 6f)
        {
            transform.localScale = new Vector3(0.2f, 0.05f, 0.2f);
        }
        else if (Time.time - startTime > 4f)
        {
            transform.localScale = new Vector3(0.2f, 0.1f, 0.2f);
        }

    }
}
