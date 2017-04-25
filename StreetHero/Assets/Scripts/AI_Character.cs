using UnityEngine;
using System.Collections;

public class AI_Character : MonoBehaviour {

    private float speed = 1f;
    private float autoSpeed = 0.018f;
    private float timeCount = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.position += new Vector3(speed * autoSpeed, 0, 0);
        if (Time.time > timeCount)
        {
            timeCount += 5;
            speed = Random.Range(0,3) - 1f;
            
        }

        if (transform.position.x > -11.7f && transform.position.x < -2.8f)
        {
            speed = speed * -1f;
        }

        if (speed == 1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (speed == -1f)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }

    }
}
