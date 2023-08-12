using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class SensorDataReceiver : MonoBehaviour
{
    internal Boolean socketReady = false;
    
    TcpClient mySocket;
    NetworkStream theStream;

    public String Host;
    public Int32 Port;

    byte[] buffer = new byte[sizeof(float)*9];

    // Define public variable for accelerometer data in x-direction
    [HideInInspector] public float accelerometerX;
    [HideInInspector] public float accelerometerY;
    [HideInInspector] public float accelerometerZ;

    public float magnetometerX;
    public float magnetometerY;
    public float magnetometerZ;

    [HideInInspector] public float gyroscopeX;
    [HideInInspector] public float gyroscopeY;
    [HideInInspector] public float gyroscopeZ;

    void Start()
    {
        setupSocket();
        Debug.Log("Socket is set up");
    }

    void Update()
    {
        // Check if data is available to read from the network stream
        if (theStream != null && theStream.DataAvailable)
        {
            int bytesRead = theStream.Read(buffer, 0, buffer.Length);
            if (bytesRead == sizeof(float)*9)
            {
                // Convert the received data to float
                accelerometerX = BitConverter.ToSingle(buffer, 0);
                accelerometerY = BitConverter.ToSingle(buffer, sizeof(float) * 1);
                accelerometerZ = BitConverter.ToSingle(buffer, sizeof(float) * 2);

                magnetometerX = BitConverter.ToSingle(buffer, sizeof(float) * 3);
                magnetometerY = BitConverter.ToSingle(buffer, sizeof(float) * 4);
                magnetometerZ = BitConverter.ToSingle(buffer, sizeof(float) * 5);

                gyroscopeX = BitConverter.ToSingle(buffer, sizeof(float) * 6);
                gyroscopeY = BitConverter.ToSingle(buffer, sizeof(float) * 7);
                gyroscopeZ = BitConverter.ToSingle(buffer, sizeof(float) * 8);
            }
        }
    }

    public void setupSocket()
    {
        try
        {
            mySocket = new TcpClient(Host, Port);
            theStream = mySocket.GetStream();
            socketReady = true;
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e);
        }
    }

    private void OnApplicationQuit()
    {
        // Clean up the TCP client and network stream when the application is closed
        if (theStream != null)
            theStream.Close();
        if (mySocket != null)
            mySocket.Close();
    }
}