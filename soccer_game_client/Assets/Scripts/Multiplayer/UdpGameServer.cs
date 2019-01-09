using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using System.Net.Sockets;

public class UdpGameServer
{
    private int m_port;
    private UdpSocket m_socket;

    public byte[] ReceivedData
    {
        get { return m_socket.ReceivedData; }
    }

    public int ReceivedDataSize
    {
        get { return m_socket.ReceivedDataSize; }
    }

    public UdpGameServer(int port)
    {
        m_port = port;
    }

    public void StartServer()
    {
        m_socket = new UdpSocket(m_port);
    }

    public void StopServer()
    {
        m_socket.Close();
    }

    public bool ReceiveData()
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);

        return m_socket.Receive(ref endPoint);
    }

    public void SendData(byte[] data)
    {
        // m_socket.Send(data)
    }
}
