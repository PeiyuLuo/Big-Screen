using UnityEngine;
using System.Collections;
using SocketIO;

public class VanController : MonoBehaviour {

    public string socketID;
    private SocketIOComponent socket;
    private float moveStep = 0.01f;
    private string movingStatu = "auto";

    public GameObject[] gameProps;

    // Use this for initialization
    void Start () {

        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();

        socket.On(socketID, GetMessage);
    }


    public void GetMessage(SocketIOEvent e)
    {
        Debug.Log(e);

        if (e.data.ToString().Equals("\"r\""))
        {
            movingStatu = "right";
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (e.data.ToString().Equals("\"l\""))
        {
            movingStatu = "left";
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        else if (e.data.ToString().Equals("\"s\""))
        {
            movingStatu = "stop";
        }
        else if (e.data.ToString().Equals("\"fire\""))
        {
            GeneratingGameProps();
        }

    }


    void Update () {

        if (movingStatu == "auto")
        {

        }
        else if (movingStatu == "left")
        {
            if ((transform.position.x < -12.2f && transform.position.x > -50f) || (transform.position.x < 51f && transform.position.x > 3f))
            {
                transform.position += new Vector3(-moveStep, 0, 0);
            }
        }
        else if (movingStatu == "right")
        {
            if ((transform.position.x < -16.2f && transform.position.x > -51f) || (transform.position.x < 50f && transform.position.x > -2f))
            {
                transform.position += new Vector3(moveStep, 0, 0);
            }
        }
        
    }

    public void GeneratingGameProps()
    {
        Debug.Log(Random.Range(0, 2) - 1);

        GameObject Fruit = (GameObject)Instantiate(gameProps[Random.Range(0, 3)], new Vector3(transform.localPosition.x, transform.localPosition.y + 0.3f, 0), Quaternion.identity);
        
        Fruit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3((Random.Range(0,3)-1)* Random.Range(3,6), 3f + Random.value*5, 0); 
    }


}
