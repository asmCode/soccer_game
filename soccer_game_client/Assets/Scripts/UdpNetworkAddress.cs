using System.Net;

public class UdpNetworkAddress : INetworkAddress
{
    public IPEndPoint EndPoint
    {
        get;
        private set;
    }

    public override string ToString()
    {
        return EndPoint.Address.ToString() + ":" + EndPoint.Port.ToString();
    }

    public override bool Equals(object obj)
    {
        return
            obj is UdpNetworkAddress &&
            EndPoint.Equals(((UdpNetworkAddress)obj).EndPoint);
    }

    public override int GetHashCode()
    {
        return EndPoint.GetHashCode();
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
