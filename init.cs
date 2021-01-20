using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class socket : MonoBehaviour
{
    public bool socketReady = false;
    TcpClient mySocket;
    public NetworkStream theStream;
    StreamWriter theWriter;
    StreamReader theReader;
    public String Host = "127.0.0.1";
    public Int32 Port = 65432;

    void Start()
    {
        setupSocket();
    }


    public void setupSocket()
    {                            // Socket setup here
        try
        {
            mySocket = new TcpClient(Host, Port);
            theStream = mySocket.GetStream();
            theWriter = new StreamWriter(theStream);
            theReader = new StreamReader(theStream);
            socketReady = true;
        }
        catch (Exception e)
        {
            Debug.Log("Socket error:" + e);                // catch any exceptions
        }
    }

    public void writeSocket(string message)
    {

        if (socketReady)
        {
            Debug.Log("message:" + message);
            theWriter.Write(message);
            theWriter.Flush();
        }
        else
        {
            setupSocket();
        }

        
    }

    public void SendMessage(List<Vector3> message)
    {
        if (socketReady == false)
        {
            return;
        }
        try
        {
            // Get a stream object for writing. 			
            if (true)
            {

                theStream = mySocket.GetStream();
                theWriter = new StreamWriter(theStream);

                string combinedString = String.Join(";", message);
                Debug.Log("data"+ combinedString);

                // Convert string message to byte array.                 
                System.Text.Encoding enc = System.Text.Encoding.UTF8;
                byte[] clientMessageAsByteArray = enc.GetBytes(combinedString);
                // Write byte array to socketConnection stream.                 
                theStream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                Debug.Log("Client sent his message - should be received by server");

            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
}
