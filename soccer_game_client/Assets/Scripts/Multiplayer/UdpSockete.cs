﻿using System.Collections;
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

    public byte[] ReceivedData
    {
        get;
        private set;
    }

    public int ReceivedDataSize
    {
        get;
        private set;
    }

    public UdpSocket(int port)
    {
        ReceivedData = new byte[BufferSize];
        ReceivedDataSize = 0;

        m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        m_socket.Blocking = false;

        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, port);
        m_socket.Bind(localEndPoint);
    }

    public bool Receive(ref IPEndPoint ipEndPoint)
    {
        if (m_socket.Available == 0)
        {
            ReceivedDataSize = 0;
            return false;
        }

        var endPoint = (EndPoint)ipEndPoint;
        ReceivedDataSize = m_socket.ReceiveFrom(ReceivedData, ref endPoint);

        return true;
    }

    public void Send(byte[] data, IPEndPoint ipEndPoint)
    {
        m_socket.SendTo(data, ipEndPoint);
    }

    public void Close()
    {
        m_socket.Close();
        m_socket.Dispose();
        m_socket = null;
    }
}
