using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MessageSerializer
{
    public abstract byte[] Serialize(Message message);
    public abstract Message Deserialize(byte[] data);
}
