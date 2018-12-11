using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class UdpConnection : Ssg.Core.Networking.Connection
{
    private const int Port = 48951;
    private const int BufferSize = 256;
    private Socket m_socket;
    private byte[] m_buffer = new byte[BufferSize];

    public UdpConnection()
    {
        m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        m_socket.Blocking = false;
    }

    public Ssg.Core.Networking.Message GetMessage()
    {
        if (m_socket.Available == 0)
            return null;

        var ipEndPoint = new IPEndPoint(IPAddress.Any, Port);
        var endPoint = (EndPoint)ipEndPoint;
        var messageSize = m_socket.ReceiveFrom(m_buffer, ref endPoint);

        var msg = new Ssg.Core.Networking.Message();
        msg.Data = new byte[messageSize];
        System.Array.Copy(m_buffer, msg.Data, messageSize);
        return msg;
    }

    public void Send(Ssg.Core.Networking.Message message)
    {
        // What is next
        // Implement it;)
    }

    public void Close()
    {
        m_socket.Close();
    }
}
