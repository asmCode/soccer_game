using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MessageSerializer
{
    public abstract byte[] Serialize(MatchMessage message);
    public abstract MatchMessage Deserialize(byte[] data);
}
