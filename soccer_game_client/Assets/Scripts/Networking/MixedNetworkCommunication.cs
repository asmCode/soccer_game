using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixedNetworkCommunication : INetworkCommunication
{
    private List<INetworkCommunication> m_netComs = new List<INetworkCommunication>();

    public void AddNetworkCommunication(INetworkCommunication netCom)
    {
        m_netComs.Add(netCom);
    }

    public void Close()
    {
        foreach (var netCom in m_netComs)
            netCom.Close();
    }

    public void Initialize()
    {
        foreach (var netCom in m_netComs)
            netCom.Initialize();
    }

    public bool Receive(byte[] data, out int size, out INetworkAddress address)
    {
        foreach (var netCom in m_netComs)
        {
            if (netCom.Receive(data, out size, out address))
                return true;
        }

        size = 0;
        address = null;

        return false;
    }

    public void Send(byte[] data, int size, INetworkAddress address)
    {
        throw new System.NotImplementedException();
    }
}
