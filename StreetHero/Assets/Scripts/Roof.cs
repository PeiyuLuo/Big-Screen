using UnityEngine;
using System.Collections;

public class Roof : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject pineappleParticle;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "fruits")
        {
            Instantiate(pineappleParticle, new Vector3(coll.transform.localPosition.x, coll.transform.localPosition.y, 0), Quaternion.identity);
            Destroy(coll.gameObject);
        }
    }
}
