using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixedNetworkCommunication : INetworkCommunication
{
    private List<INetworkCommunication> m_netComs = new List<INetworkCommunication>();
    private INetworkCommunication m_defaultSendCom;
    private Dictionary<INetworkAddress, INetworkCommunication> m_addrToCom =
        new Dictionary<INetworkAddress, INetworkCommunication>();

    public void AddNetworkCommunication(INetworkCommunication netCom, INetworkAddress address)
    {
        m_netComs.Add(netCom);

        if (address == null)
            m_defaultSendCom = netCom;
        else
            m_addrToCom[address] = netCom;
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
        INetworkCommunication netCom;
        if (!m_addrToCom.TryGetValue(address, out netCom))
            netCom = m_defaultSendCom;

        netCom.Send(data, size, address);
    }
}
