using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController
{
    public MessageQueue MessageQueue { get; private set; }

    public NetworkController()
    {
        MessageQueue = new MessageQueue();
    }

    public void SendMessage(Message message)
    {
        MessageQueue.AddMessage(message);
    }
}
