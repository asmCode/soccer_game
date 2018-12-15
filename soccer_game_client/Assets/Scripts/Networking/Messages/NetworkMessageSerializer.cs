using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ssg.Core.Networking;

public interface NetworkMessageSerializer
{
    byte[] Serialize(NetworkMessage message);
    NetworkMessage Deserialize(byte[] data);
}
