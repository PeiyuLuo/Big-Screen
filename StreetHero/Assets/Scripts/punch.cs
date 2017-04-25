using UnityEngine;
using System.Collections;

public class punch : MonoBehaviour {
    private float stageBoundary_Left = -11.9f;
    private float stageBoundary_Right = -3.0f;
    // Use this for initialization
    //public bool deathMode = false;

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.name);
        //Debug.Log(coll.transform.position.x);
        //Debug.Log(transform.position.x);
        if (coll.collider.tag == "man")
        {
            if(coll.transform.position.x < stageBoundary_Right && coll.transform.position.x > stageBoundary_Left)
            {
                if (GameManager.deathMode)
                {            
                    StartCoroutine(KillThis(coll.transform.gameObject)); 
                }
                if (transform.position.x < coll.transform.position.x)
                {
                    coll.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(8f, 4f, 0);
                }
                else
                {
                    coll.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-8f, 4f, 0);
                }
                coll.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 500f;
            }
            
        }
           
    }///end void on collision

    public IEnumerator KillThis(GameObject go)
    {

        yield return new WaitForSeconds(0.8f);
        go.SetActive(false);

        yield return new WaitForSeconds(0.15f);
        go.SetActive(true);

        yield return new WaitForSeconds(0.15f);
        go.SetActive(false);

        yield return new WaitForSeconds(0.15f);
        go.SetActive(true);

        yield return new WaitForSeconds(0.15f);
        go.SetActive(false);

        yield return new WaitForSeconds(0.15f);
        go.SetActive(true);


        yield return new WaitForSeconds(0.3f); 
        go.SetActive(false);
    }

}

   


