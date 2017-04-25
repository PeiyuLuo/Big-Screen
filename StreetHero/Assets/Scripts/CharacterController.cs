using UnityEngine;
using System.Collections;
using SocketIO;

public class CharacterController : MonoBehaviour
{
    private float stageBoundary_Left = -11.6f;
    private float stageBoundary_Right = -3.0f;

    public string socketID;
    public float Left0_Right1;
    
    private SocketIOComponent socket;
    private float moveStep = 0.06f;
    private string movingStatu = "auto";

    private Animator anim;
    private bool HoldingSomething = false;
    private bool onBoxingStage = false;
    private float jumpSpeed = 5.5f;

    private Rigidbody2D rb;
    public GameObject WatermelonPartical;

    private bool stepOnBananaPeel = false;
    private bool pickupFruits = false;

   // public bool deathMode = false;

    public GameObject endingParticle;


    public void Start()
    {
        //GetComponent<Renderer>().sortingLayerName = "winners";
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();

        socket.On(socketID, GetMessage);
    }


    public void GetMessage(SocketIOEvent e)
    {
       // Debug.Log(e);

        if (e.data.ToString().Equals("\"u\""))
        {
            movingStatu = "up";
        }
        else if (e.data.ToString().Equals("\"r\""))
        {
            if (!stepOnBananaPeel) {
                movingStatu = "right";
                if (onBoxingStage) { anim.SetInteger("walkingStatus", 5); }
                else { anim.SetInteger("walkingStatus", 1); }

                transform.rotation = Quaternion.Euler(0, 0, 0);
            }            
        }
        else if (e.data.ToString().Equals("\"l\""))
        {
            if (!stepOnBananaPeel)
            {
                movingStatu = "left";
                if (onBoxingStage) { anim.SetInteger("walkingStatus", 5); }
                else { anim.SetInteger("walkingStatus", 1); }

                transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
        }
        else if (e.data.ToString().Equals("\"s\""))
        {
            movingStatu = "stop";
            if (onBoxingStage)
            {
                anim.SetInteger("walkingStatus", 3);
            }
            else
            {
                anim.SetInteger("walkingStatus", 0);
            }
           
        }
        else if (e.data.ToString().Equals("\"fire\""))
        {
            if (onBoxingStage)
            {
                punch();
            }
            else
            {
                ThrowObjects();
            }
        }
        else if (e.data.ToString().Equals("\"reset\""))
        {
            resetPosition();
        }

    }



    Rigidbody2D childRB;
    //function for throwing fruits or any other object
    private void ThrowObjects()
    {
        Debug.Log("firing!!!!");
        anim.SetInteger("walkingStatus", 2);
        childRB = transform.GetChild(2).gameObject.GetComponent<Rigidbody2D>();
        childRB.isKinematic = false;
        
        if (transform.localRotation.y == 0) { childRB.velocity = new Vector3(5f, 10f+ Random.value*2, 0); } // random throwing height /// makes user believe they could control the power
        else if (transform.localRotation.y == -1) { childRB.velocity = new Vector3( -5f, 10f+ Random.value*2, 0); } //reverse direction
        childRB.angularVelocity = 500f;
        

        StartCoroutine(throwAwayThings()); //reset later to prevent conflict
    }

    public IEnumerator throwAwayThings()
    {
        childRB.gameObject.transform.SetParent(transform.parent);
        yield return new WaitForSeconds(0.6f); // waits 0.3 seconds
        HoldingSomething = false;
        
        socket.Emit(socketID, "gyroOff"); // tell phone-node that the fruit is alreay thrown away
    }

    //function for punch - fight on the stage
    private void punch()
    {
        //Debug.Log("aa");
        anim.SetInteger("walkingStatus", 4);
        StartCoroutine(changeAnimationBack());
    }

    public IEnumerator changeAnimationBack()
    {
        yield return new WaitForSeconds(0.6f); // waits 0.8 seconds
        anim.SetInteger("walkingStatus", 3);
    }



    void Update()
    {

        if (GameManager.EndingExplosion)
        {
            Instantiate(endingParticle, new Vector3(transform.localPosition.x, transform.localPosition.y + 0.5f, 0), Quaternion.identity);
            Destroy(transform.gameObject);
        }
        ////check if move to boxing stage
        if (transform.position.x > stageBoundary_Left && transform.position.x < stageBoundary_Right && rb.isKinematic) { 
            rb.isKinematic = false;
            rb.velocity = new Vector3(0, jumpSpeed, 0);
            transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            moveStep = 0.03f;
            anim.SetInteger("walkingStatus", 3);
            onBoxingStage = true;

            if (HoldingSomething) // Throw out fuits
            {
                childRB = transform.GetChild(2).gameObject.GetComponent<Rigidbody2D>();
                childRB.transform.localPosition += new Vector3(0, 4f, 0);  //prevent rigidbodys collision
                childRB.isKinematic = false;

                if (transform.localRotation.y == 0) { childRB.velocity = new Vector3(-5f, 5f + Random.value * 2, 0); } // random throwing height
                else if (transform.localRotation.y == -1) { childRB.velocity = new Vector3(5f, 5f + Random.value * 2, 0); } //reverse direction
                childRB.angularVelocity = 500f;

                StartCoroutine(throwAwayThings());
            }

        }///end if move to boxing stage


            if (movingStatu == "up")
            {
                transform.position += new Vector3(0, moveStep / 2, 0);
            }
            else if (movingStatu == "left")
            {
                if (transform.position.x > -53)
                {
                    transform.position += new Vector3(-moveStep, 0, 0);
                }
            }
            else if (movingStatu == "right")
            {
                if (transform.position.x < 53)
                {
                    transform.position += new Vector3(moveStep, 0, 0);
                }
            }
        }//end void update

    void resetPosition()
    {
        rb.isKinematic = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        if (Left0_Right1 == 0)
        {
            transform.localPosition = new Vector3(-28f, -0.5f, 0);
           
        }
        else if(Left0_Right1 == 1)
        {
            transform.localPosition = new Vector3(26.7f, -0.5f, 0);
        }
        rb.velocity = new Vector3(Random.value, 1f, 0);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //Debug.Log(coll.gameObject.name);
        if (coll.gameObject.name == "Watermelon(Clone)")
        {
            Destroy(coll.gameObject);
            Instantiate(WatermelonPartical, new Vector3(transform.localPosition.x, transform.localPosition.y + 0.5f, 0), Quaternion.identity);
            
            transform.rotation = Quaternion.Euler(0, 0, -90f);
            rb.isKinematic = false;

        }
        else if (coll.gameObject.name == "Ground")
        {
            rb.isKinematic = true;
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            moveStep = 0.06f;
            onBoxingStage = false;
        }
        else if (coll.gameObject.name == "Banana_Peels(Clone)" || coll.gameObject.name == "Banana_Peels") /////Banana Peel
        {
            //Then fall down   
            movingStatu = "stop";
            stepOnBananaPeel = true;
            if (transform.localRotation.y == 0) {
                transform.rotation = Quaternion.Euler(0, 0, -90f);
                rb.velocity = new Vector3(2f, 3f, 0);
                rb.angularVelocity = 300f;
                rb.isKinematic = false;
                coll.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-4f, 0.5f ,0);
            } 
            else if (transform.localRotation.y == -1)
            {
                transform.rotation = Quaternion.Euler(0, -180f, -90f);
                rb.velocity = new Vector3(-2f, 3f, 0);
                rb.angularVelocity = 300f;
                rb.isKinematic = false;
                coll.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(4f, 0.5f, 0);
            } //reverse direction
            
            StartCoroutine(Standup());
            //coll.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-3f, 0.5f, 0);

        }

        if (onBoxingStage)
        {
            if (coll.gameObject.name == "Punch")
            {
                Debug.Log("hit!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
        }


        
        if (!HoldingSomething)
        {
            //picking up fruits;
            //if (coll.gameObject.name == "Pineapple" || coll.gameObject.name == "Pineapple(Clone)")
            if(coll.collider.tag == "fruits")
            {
                //pickupFruits = false;
                //Debug.Log("find Pineapple");
                HoldingSomething = true;
                //reset the object position where the hand is
                coll.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                coll.gameObject.transform.SetParent(transform);
                coll.gameObject.transform.localPosition = new Vector3(0.32f, -0.60f, 1);
                coll.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 140f);

                socket.Emit(socketID, "gyroOn"); // tell phone-node that they just pick a fruit
            }
  
        }///end if NOT hoding something
        else
        {
            if (coll.collider.tag == "fruits")
            {
                //coll.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            }

        }
        Debug.Log(coll.collider.tag);
    }///end on void collision

    void OnCollisionExit2D(Collision2D coll)
    {

    }

    public IEnumerator Standup()
    {
        yield return new WaitForSeconds(3f); // waits 0.8 seconds
        stepOnBananaPeel = false;
        transform.rotation = Quaternion.Euler(0, 180f, 0);
        transform.localPosition = new Vector3(transform.position.x, -1.19f, 0);
    }
}



