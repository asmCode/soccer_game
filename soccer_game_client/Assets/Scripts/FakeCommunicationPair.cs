using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCommunicationPair
{
    private Queue<DataAndAddress> m_queue1 = new Queue<DataAndAddress>();
    private Queue<DataAndAddress> m_queue2 = new Queue<DataAndAddress>();

    private FakeCommunication m_comA;
    private FakeCommunication m_comB;

    public FakeCommunicationPair()
    {
        m_comA = new FakeCommunication(m_queue1, m_queue2);
        m_comB = new FakeCommunication(m_queue2, m_queue1);
    }

    public INetworkCommunication GetComA()
    {
        return m_comA;
    }

    public INetworkCommunication GetComB()
    {
        return m_comB;
    }
}
