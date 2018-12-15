using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using System.Net.Sockets;

public class UdpGameServer : GameServer
{
    private int m_port;
    private UdpSocket m_socket;

    public UdpGameServer(int port)
    {
        m_port = port;
    }

    public override byte[] RecvMessage()
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, m_port);
        return m_socket.Receive(ref endPoint);
    }

    public override void StartServer()
    {
        m_socket = new UdpSocket(m_port);
    }

    public override void StopServer()
    {
        m_socket.Close();
    }
}
