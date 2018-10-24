using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageQueue
{
    private Queue<Message> m_messages = new Queue<Message>();

    public bool Empty()
    {
        return m_messages.Count == 0;
    }

    public void Clear()
    {
        m_messages.Clear();
    }

    public void AddMessage(Message message)
    {
        m_messages.Enqueue(message);
    }

    public Message Dequeue()
    {
        if (Empty())
            return null;

        return m_messages.Dequeue();
    }
}
