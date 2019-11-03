using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCommunication : INetworkCommunication
{
    private Queue<DataAndAddress> m_sendQueue;
    private Queue<DataAndAddress> m_recvQueue;

    public FakeCommunication(Queue<DataAndAddress> sendQueue, Queue<DataAndAddress> recvQueue)
    {
        m_sendQueue = sendQueue;
        m_recvQueue = recvQueue;
    }

    public void Close()
    {
    }

    public void Initialize()
    {
    }

    public bool Receive(byte[] data, out int size, out INetworkAddress address)
    {
        if (m_recvQueue.Count > 0)
        {
            var dataAndAddr = m_recvQueue.Dequeue();
            dataAndAddr.data.CopyTo(data, out size);
            address = dataAndAddr.address;
            return true;
        }

        size = 0;
        address = null;
        return false;
    }

    public void Send(byte[] data, int size, INetworkAddress address)
    {
        var dataAndAddr = new DataAndAddress()
        {
            data = new RawData(data, size),
            address = address
        };

        m_sendQueue.Enqueue(dataAndAddr);
    }
}
