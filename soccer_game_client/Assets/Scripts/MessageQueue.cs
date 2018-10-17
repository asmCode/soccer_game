using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageQueue
{
    private List<Message> m_messages = new List<Message>();

    public void AddMessage(Message message)
    {
        m_messages.Add(message);
    }

    public void ProcessMessages()
    {

    }
}
