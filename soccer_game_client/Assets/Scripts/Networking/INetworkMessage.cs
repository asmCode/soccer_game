using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkMessage
{
    public NetworkMessageType m_type;
    public object m_msg;
    //public abstract NetworkMessageType MessageType { get; }
    //public abstract void GetData(IDataWriter dataWriter);
    //public abstract void SetData(IDataReader dataReader);

    //public byte[] Serialize()
    //{
    //    return null;
    //}

    //public NetworkMessage Deserialize(byte[] data)
    //{
    //    return null;
    //}
}
