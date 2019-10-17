using System.Net;

public class UdpCommunication : INetworkCommunication
{
    private UdpSocket m_socket;
    private int m_localPort;

    public UdpCommunication(int localPort)
    {
        m_localPort = localPort;
    }

    public void Initialize()
    {
        m_socket = new UdpSocket(m_localPort);
    }

    public void Send(byte[] data, int size, INetworkAddress address)
    {
        var udpAddress = address as UdpNetworkAddress;
        m_socket.Send(data, size, udpAddress.EndPoint);
    }

    public bool Receive(byte[] data, out int size, out INetworkAddress address)
    {
        var endPoint = new IPEndPoint(IPAddress.Any, 0);
        if (!m_socket.Receive(data, out size, ref endPoint))
        {
            address = null;
            return false;
        }

        address = new UdpNetworkAddress(endPoint);

        return true;
    }

    public void Close()
    {
        m_socket.Close();
    }
}
