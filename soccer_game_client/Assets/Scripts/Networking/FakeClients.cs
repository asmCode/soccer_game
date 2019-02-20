using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeClients : MonoBehaviour
{
    // private GameServer m_gameServer;

    private FakeClient m_client1;
    private FakeClient m_client2;
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
        m_client1.Update(Time.deltaTime);
        m_client2.Update(Time.deltaTime);
    }

    private void Initialize()
    {
        m_client1 = new FakeClient(new MouseAndKbInput(), "Clien 1", "client-1-address");
        m_client2 = new FakeClient(null, "Clien 2", "client-2-address");

        m_netCom = new FakeServerNetworkCommunication(m_client1, m_client2);

        // m_gameServer = GameObject.Find("ServerScene").GetComponent<ServerScene>().GameServer;
    }

    public void UIEventClien1Join()
    {
        m_client1.SendJoinRequest();
    }

    public void UIEventClien2Join()
    {
        m_client2.SendJoinRequest();
    }

    public void UIEventClien1ReadyToStart()
    {
        m_client1.SendReadyToStart();
    }

    public void UIEventClien2ReadyToStart()
    {
        m_client2.SendReadyToStart();
    }
}
