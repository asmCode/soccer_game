using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeServer: MonoBehaviour
{
    private FakeServerNetworkCommunication m_netCom;

    public INetworkCommunication NetworkCommunication
    {
        get { return m_netCom; }
    }

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
    }

    private void Initialize()
    {
        //m_client1 = new FakeClient(new MouseAndKbInput(), "Clien 1", "client-1-address");
        //m_client2 = new FakeClient(null, "Clien 2", "client-2-address");

        m_netCom = new FakeServerNetworkCommunication();

        // m_gameServer = GameObject.Find("ServerScene").GetComponent<ServerScene>().GameServer;
    }
}
