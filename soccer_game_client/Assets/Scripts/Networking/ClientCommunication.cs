using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

// TODO: this need to inherit from the interface
public class ClientCommunication
{
    private UdpSocket m_socket;
    private IPEndPoint m_serverEndPoint;

    public ClientCommunication(string serverIp, int serverPort)
    {
        m_socket = new UdpSocket(0);
        m_serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);
    }

    public void SendMessage(byte[] data)
    {
        m_socket.Send(data, m_serverEndPoint);
    }

    public Message GetMessage()
    {
        return null;
    }

    public void Close()
    {

    }
}
