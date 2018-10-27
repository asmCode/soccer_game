using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NetworkCommunication
{
    public abstract void SendMessage(Message message);
    public abstract Message GetMessage();
}
