using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;

public class UDP_client : MonoBehaviour
{
    public string HOST = "127.0.0.1"; // default local
    public int PORT = 12345;
    UdpClient client;
    IPEndPoint remoteEndPoint;

    void Start()
    {
        Setup();
    }

    public void Setup()
    {
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(HOST), PORT);
        client = new UdpClient();
    }

    public void sendData(string data)
    {
        try
        {
            
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            client.Send(dataBytes, dataBytes.Length, remoteEndPoint);
            Debug.Log("Client sent his message - should be received by server");

        }

        catch (Exception err)
        {
            print(err.ToString());
        }
    }

    

  
}
