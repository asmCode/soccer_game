using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class UdpClientAddress : ClientAddress
{
    public IPEndPoint EndPoint
    {
        get;
        private set;
    }

    public UdpClientAddress(IPEndPoint endPoint)
    {
        EndPoint = endPoint;
    }

    public override string GetAddressName()
    {
        return EndPoint.Address.ToString();
    }

    public override ClientAddress Clone()
    {
        throw new System.NotImplementedException();
    }
}
