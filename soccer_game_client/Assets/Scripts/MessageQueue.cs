using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageQueue
{
    private Queue<MatchMessage> m_messages = new Queue<MatchMessage>();

    public bool Empty()
    {
        return m_messages.Count == 0;
    }

    public void Clear()
    {
        m_messages.Clear();
    }

    public void AddMessage(MatchMessage message)
    {
        m_messages.Enqueue(message);
    }

    public MatchMessage Dequeue()
    {
        if (Empty())
            return null;

        return m_messages.Dequeue();
    }
}
