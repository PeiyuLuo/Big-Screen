using UnityEngine;
using System.Collections;

public class GroundManager : MonoBehaviour {

    public GameObject WatermelonPartical;
    public GameObject WatermelonPartical_Green;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject pineappleParticle;

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.name == "Watermelon(Clone)")
        {
            //  Debug.Log("booooommm");
            Instantiate(WatermelonPartical_Green, new Vector3(coll.transform.localPosition.x, coll.transform.localPosition.y + 0.5f, 0), Quaternion.identity);
            Instantiate(WatermelonPartical, new Vector3(coll.transform.localPosition.x, coll.transform.localPosition.y + 0.5f, 0), Quaternion.identity);
            Destroy(coll.gameObject);
        }
        //picking up fruits;
        else if (coll.gameObject.name == "Pineapple" || coll.gameObject.name == "Pineapple(Clone)" || coll.gameObject.name == "Banana" || coll.gameObject.name == "Banana(Clone)")
            {
                Debug.Log("pineapple on the ground");
                
            //coll.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }

    
            
       

    }

}
