﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class NetTest : MonoBehaviour
{
    int port = 48752;

    Socket m_serverSocket;
    Socket m_clientSocket;

    EndPoint clientEndPoint;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartServer()
    {
        Debug.Log("StartServer");

        m_serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        m_serverSocket.Blocking = false;

        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, port);
        m_serverSocket.Bind(localEndPoint);
    }

    public void StartClient()
    {
        Debug.Log("StartClient");

        m_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        m_clientSocket.Blocking = false;

        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 0);
        m_clientSocket.Bind(localEndPoint);
        Debug.Log(((IPEndPoint)m_clientSocket.LocalEndPoint).Port.ToString());
    }

    public void SendToClient()
    {
        Debug.Log("SendToClient");

        m_serverSocket.SendTo(new byte[4] { 1, 2, 3, 4 }, clientEndPoint);
    }

    public void SendToServer()
    {
        Debug.Log("SendToServer");

        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.0.110"), port);

        m_clientSocket.SendTo(new byte[3] { 1, 2, 3 }, endPoint);
    }

    public void ReceiveFromClient()
    {
        if (m_serverSocket.Available == 0)
        {
            Debug.Log("Not available");
            return;
        }

        var data = new byte[256];
        IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 0);
        clientEndPoint = (EndPoint)ipEndPoint;
        int size = m_serverSocket.ReceiveFrom(data, ref clientEndPoint);

        Debug.LogFormat("Received {0} bytes from {1}:{2}", size, ((IPEndPoint)clientEndPoint).Address.ToString(), ((IPEndPoint)clientEndPoint).Port);
    }

    public void ReceiveFromServer()
    {
        if (m_clientSocket.Available == 0)
        {
            Debug.Log("Not available");
            return;
        }

        var data = new byte[256];
        IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 0);
        EndPoint endPoint = (EndPoint)ipEndPoint;
        int size = m_clientSocket.ReceiveFrom(data, ref endPoint);

        Debug.LogFormat("Received {0} bytes from {1}:{2}", size, ((IPEndPoint)endPoint).Address.ToString(), ((IPEndPoint)endPoint).Port);
    }
}
