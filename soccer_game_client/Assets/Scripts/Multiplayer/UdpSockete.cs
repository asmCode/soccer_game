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
    // private const int BufferSize = 256;
    private Socket m_socket;

    //public byte[] ReceivedData
    //{
    //    get;
    //    private set;
    //}

    //public int ReceivedDataSize
    //{
    //    get;
    //    private set;
    //}

    public UdpSocket(int port)
    {
        //ReceivedData = new byte[BufferSize];
        //ReceivedDataSize = 0;

        m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        m_socket.Blocking = false;

        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, port);
        m_socket.Bind(localEndPoint);
    }

    public bool Receive(byte[] data, out int size, ref IPEndPoint ipEndPoint)
    {
        if (m_socket.Available == 0)
        {
            size = 0;
            return false;
        }

        var endPoint = (EndPoint)ipEndPoint;
        size = m_socket.ReceiveFrom(data, ref endPoint);
        ipEndPoint = (IPEndPoint)endPoint;

        return true;
    }

    public void Send(byte[] data, int size, IPEndPoint ipEndPoint)
    {
        m_socket.SendTo(data, size, SocketFlags.None, ipEndPoint);
    }

    public void Close()
    {
        m_socket.Close();
        m_socket = null;
    }
}
