using UnityEngine;
using System.Collections;
using SocketIO;
using UnityEngine.UI;

public class UI_text_control : MonoBehaviour
{

    private SocketIOComponent socket;
    public string socketID;
    //public string link;

    public void Start()
    {
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();

        socket.On(socketID, TestMessage);

    }

    

    public void TestMessage(SocketIOEvent e)
    {
        string s = e.data.ToString();
        Debug.Log(s.Substring(1,s.Length -2));
        GetComponent<Text>().text = s.Substring(1, s.Length - 2);

    }

    
}
