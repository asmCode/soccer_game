using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Ssg.Core.Networking;

public class UdpSocket
{
    private const int BufferSize = 256;
    private Socket m_socket;
    private byte[] m_buffer = new byte[BufferSize];
    private int m_port;

    public UdpSocket(int port)
    {
        m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        m_socket.Blocking = false;
        m_port = port;

        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, port);
        m_socket.Bind(localEndPoint);
    }

    public byte[] Receive(ref IPEndPoint ipEndPoint)
    {
        if (m_socket.Available == 0)
        {
            Debug.Log("Not available");
            return null;
        }

        Debug.Log("Available");

        var endPoint = (EndPoint)ipEndPoint;
        var messageSize = m_socket.ReceiveFrom(m_buffer, ref endPoint);

        var data = new byte[messageSize];
        System.Array.Copy(m_buffer, data, messageSize);
        return data;
    }

    public void Send(byte[] data, IPEndPoint ipEndPoint)
    {
        m_socket.SendTo(data, ipEndPoint);
    }

    public void Close()
    {
        m_socket.Close();
    }
}
