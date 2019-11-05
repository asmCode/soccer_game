using UnityEngine;
using System.Collections.Generic;

class PostMsg
{
    public float m_timeToSend;
    public byte[] m_data;
    public INetworkAddress m_address;
}

public class DelayedNetworkCommunication : INetworkCommunication
{
    private const float m_delay = 0.3f;
    private INetworkCommunication m_com;
    private Queue<PostMsg> m_postMsgQueue = new Queue<PostMsg>();

    public DelayedNetworkCommunication(INetworkCommunication com)
    {
        m_com = com;
        Updater.Create("DelayedNetworkCommunicationUpdater", Update);
    }

    public void Initialize()
    {
        m_com.Initialize();
    }

    public void Send(byte[] data, int size, INetworkAddress address)
    {
        var postMsg = new PostMsg();
        postMsg.m_timeToSend = Time.time + m_delay;
        postMsg.m_data = new byte[size];
        postMsg.m_address = address;
        System.Array.Copy(data, postMsg.m_data, size);

        m_postMsgQueue.Enqueue(postMsg);
    }

    public bool Receive(byte[] data, out int size, out INetworkAddress address)
    {
        return m_com.Receive(data, out size, out address);
    }

    public void Close()
    {
        m_com.Close();
    }

    private void Update()
    {
        while (true)
        {
            if (m_postMsgQueue.Count == 0)
                break;

            var msg = m_postMsgQueue.Peek();
            if (msg.m_timeToSend >= Time.time)
                break;

            m_com.Send(msg.m_data, msg.m_data.Length, msg.m_address);
            m_postMsgQueue.Dequeue();
        }
    }
}
