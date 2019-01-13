//using System.Net;

//public class UdpGameServer
//{
//    private int m_port;
//    private UdpSocket m_socket;
//    private UdpClientAddress m_clientAddress;

//    public byte[] ReceivedData
//    {
//        get { return m_socket.ReceivedData; }
//    }

//    public int ReceivedDataSize
//    {
//        get { return m_socket.ReceivedDataSize; }
//    }

//    public ClientAddress ClientAddress
//    {
//        get { return m_clientAddress; }
//    }

//    public UdpGameServer(int port)
//    {
//        m_port = port;
//    }

//    public void StartServer()
//    {
//        m_socket = new UdpSocket(m_port);
//    }

//    public void StopServer()
//    {
//        m_socket.Close();
//    }

//    public bool ReceiveData()
//    {
//        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);

//        if (!m_socket.Receive(ref endPoint))
//            return false;

//        m_clientAddress = new UdpClientAddress(endPoint);

//        return true;
//    }

//    public void SendData(byte[] data, ClientAddress clientAddress)
//    {
//        var udpClientAddress = clientAddress as UdpClientAddress;
//        if (udpClientAddress == null)
//            return;

//        m_socket.Send(data, udpClientAddress.EndPoint);
//    }
//}
