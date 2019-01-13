using System.Net;

public class UdpNetworkAddress : INetworkAddress
{
    public IPEndPoint EndPoint
    {
        get;
        private set;
    }

    public UdpNetworkAddress(IPEndPoint ipEndPoint)
    {
        EndPoint = ipEndPoint;
    }

    public UdpNetworkAddress(string ip, int port)
    {
        EndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
    }
}
