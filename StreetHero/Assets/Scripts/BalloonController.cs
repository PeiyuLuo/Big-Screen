using UnityEngine;
using System.Collections;
using SocketIO;

public class BalloonController : MonoBehaviour
{
    public string socketID;
    public float Left0_Right1;
    private SocketIOComponent socket;
    private string movingStatu = "auto";
    
    private float autoSpeed = 0.002f;
    float force = 0.008f;
    Vector3 velocity = Vector3.zero;
    private float maxSpeed = 0.035f;

    private float cdTime = 3f;
    private float lastFireTime = 5;
    private bool readyToFire = false;


    Vector3 forceVector = Vector3.zero;
    Vector3 acceleration = Vector3.zero;
    Vector3 resistance = Vector3.zero;

    public GameObject particlePrefab;
    public GameObject[] bomb;

    private bool onGround = false;
    public GameObject endingParticle;

    public void Start()
    {
        //Debug.Log(bomb.Length);
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();

        socket.On(socketID, GetMessage);
    }

    public void GetMessage(SocketIOEvent e)
    {
        Debug.Log(e);
        if (!onGround) {
            if (e.data.ToString().Equals("\"u\""))
            {
                movingStatu = "up";
            }
            else if (e.data.ToString().Equals("\"d\""))
            {
                movingStatu = "down";
            }
            else if (e.data.ToString().Equals("\"r\""))
            {
                movingStatu = "right";
            }
            else if (e.data.ToString().Equals("\"l\""))
            {
                movingStatu = "left";
            }
            else if (e.data.ToString().Equals("\"s\""))
            {
                movingStatu = "stop";
            }
            else if (e.data.ToString().Equals("\"fire\""))
            {
                if (transform.GetChild(0).localScale.x < 1) {
                    Debug.Log("refill");
                    RefillBalloon();
                }
                else
                {
                    //StartCoroutine(Upward());
                    if (readyToFire) { fireBall(); }
                    
                }
            }
            else if (e.data.ToString().Equals("\"reset\""))
            {
                resetPosition();
            }
        }//end if not on the ground
        else
        {
            if (e.data.ToString().Equals("\"fire\""))
            {
                if (transform.GetChild(0).localScale.x < 1)
                {
                    Debug.Log("refill");
                    RefillBalloon();
                }
                else
                {
                    GetComponent<Rigidbody2D>().isKinematic = true;
                    StartCoroutine(Upward());
                }
            }
        }

    }

    public IEnumerator Upward()
    {
        movingStatu = "up";
        onGround = false;
        yield return new WaitForSeconds(0.9f); // waits 0.9 seconds
        movingStatu = "stop";
    }

    private float speed = 1f;
    private float timeCount = 0;
    private float scaleup = 0;

    void Update()
    {
        //
        if (GameManager.EndingExplosion)
        {
            Instantiate(endingParticle, new Vector3(transform.localPosition.x, transform.localPosition.y + 0.5f, 0), Quaternion.identity);
            Destroy(transform.gameObject);
        }


        if (Time.time - lastFireTime > cdTime && !readyToFire)
        {
            socket.Emit(socketID, "gyroOn");
            readyToFire = true;
        }
        if (Input.GetKeyDown("e"))
        {
            Debug.Log("EXPLODE!");
            BalloonExplode();
            
        }
        if (Input.GetKeyDown("space"))
        {
            RefillBalloon();
        }
        else if (Input.GetKeyDown("b"))
        {
            fireBall();
        }



        if (movingStatu == "auto")
        {
            transform.position += new Vector3(speed * autoSpeed, 0, 0);
            if (Time.time > timeCount)
            {
                timeCount += 5;
                if (Random.value > 0.5)
                {
                    speed = 1;
                }
                else
                {
                    speed = -1;
                }
            }
        }
        else
        {
            if (scaleup == 0)///if it is the first tiem to login
            {
                scaleup = 1;
                lastFireTime = Time.time + 10f;
            }
            else if (scaleup == 1)
            {
                if (transform.localScale.x < 0.5)
                {
                    transform.localScale += new Vector3(0.01f, 0.01f, 0);
                }
                else
                {
                    scaleup = 2;
                }
            }
            else if (scaleup == 2)
            {
                if (transform.localScale.x > 0.4)
                {
                    transform.localScale -= new Vector3(0.01f, 0.01f, 0);
                }
                else
                {
                    scaleup = 3;
                }
            }

            if (movingStatu == "up" && transform.localPosition.y < 2.8f)
            {
                forceVector = new Vector3(0, force, 0);
            }
            else if (movingStatu == "down" && transform.localPosition.y > 2.1f)
            {
                forceVector = new Vector3(0, -force, 0);
            }
            else if (movingStatu == "left" && transform.localPosition.x > -53f)
            {
                forceVector = new Vector3(-force, 0, 0);
            }
            else if (movingStatu == "right" && transform.localPosition.x < 53f)
            {
                forceVector = new Vector3(force, 0, 0);
            }
            applyForce();
            move();
        }
    }

    void applyForce()
    {
        acceleration += forceVector;
        if (acceleration == Vector3.zero)
        {
            float resist = 0.1f * force;
            if (velocity.x < 0)
            {
                resistance += resist * Vector3.right;
            }
            if (velocity.x > 0)
            {
                resistance += resist * Vector3.left;
            }
            if (velocity.y < 0)
            {
                resistance += resist * Vector3.up;
            }
            if (velocity.y > 0)
            {
                resistance += resist * Vector3.down;
            }
            acceleration += resistance;
        }
        forceVector = Vector3.zero;
        resistance = Vector3.zero;
    }

    void move()
    {
        velocity += acceleration;
        if (velocity.magnitude > maxSpeed)
        {
            velocity = maxSpeed * velocity.normalized;
        }
        transform.position += velocity;
        acceleration = Vector3.zero;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        ////hit by objects
        if (coll.gameObject.name == "Pineapple" || coll.gameObject.name == "Pineapple(Clone)")
        {
            if (!onGround)
            {
                BalloonExplode();
                if (GameManager.deathMode)
                {
                    Destroy(transform.gameObject);
                }
                
            }
           
        }
        else if (coll.gameObject.name == "Ground")
        {
            //// if balloon fall on the ground
            Debug.Log("hit ground!!");
            onGround = true;
            GetComponent<Rigidbody2D>().isKinematic = true;
        }

    }


    public void BalloonExplode()
    {
        Instantiate(particlePrefab, new Vector3(transform.localPosition.x, transform.localPosition.y + 0.8f, 0), Quaternion.identity);
        transform.GetChild(0).localScale = new Vector3(0.1f, 0.1f, 0.1f);
        transform.GetChild(1).localScale = new Vector3(1.9f, 1.9f, 1.9f);
        GetComponent<Rigidbody2D>().isKinematic = false;
        movingStatu = "stop";
    }


    public void RefillBalloon()
    {
        transform.GetChild(0).localScale += new Vector3(0.15f, 0.15f, 0.15f);
        transform.GetChild(1).localScale -= new Vector3(0.15f, 0.15f, 0.15f);
    }

    public void fireBall()
    {
        lastFireTime = Time.time;
        readyToFire = false;
        socket.Emit(socketID, "gyroOff");
        //Debug.Log();
        Instantiate(bomb[Random.Range(0, bomb.Length)], new Vector3(transform.localPosition.x, transform.localPosition.y, 0), Quaternion.identity);
    }

    void resetPosition()
    {
        //transform.rotation = Quaternion.Euler(0, 0, 0);
        if (Left0_Right1 == 0)
        {
            transform.localPosition = new Vector3(-30.1f + Random.value, 2.9f, 0);

        }
        else if (Left0_Right1 == 1)
        {
            transform.localPosition = new Vector3(24.5f + Random.value, 2.9f, 0);
        }
        
    }

}
