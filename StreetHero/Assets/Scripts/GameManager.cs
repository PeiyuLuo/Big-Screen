#region License
#endregion

using UnityEngine;
using System.Collections;
using SocketIO;

public class GameManager : MonoBehaviour
{
    private SocketIOComponent socket;
    public static bool deathMode = false;
    public static bool EndingExplosion = false;

    public GameObject blackCover;

    public void Start()
    {

        Screen.SetResolution(11520, 1080, false); //set screen resolution

        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();
        socket.On("open", TestOpen);        
    }

    void Update()
    {
        if (!deathMode && Time.time > 162.7f)
        {
            deathMode = true;
        }

        if (blackCover.GetComponent<Black>().explode && !EndingExplosion)
        {
            EndingExplosion = true;
        }

    }

    public void TestOpen(SocketIOEvent e)
    {
        socket.Emit("reset", "reset");
    }



}
