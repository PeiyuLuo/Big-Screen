using UnityEngine;
using System.Collections;

public class FruitExplode : MonoBehaviour {

    public GameObject particle;


	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "fruits" && !GetComponent<Rigidbody2D>().isKinematic)
        {
            Instantiate(particle, new Vector3(transform.localPosition.x, transform.localPosition.y + 0.5f, 0), Quaternion.identity);
            Destroy(transform.gameObject);
        }
    }
}
