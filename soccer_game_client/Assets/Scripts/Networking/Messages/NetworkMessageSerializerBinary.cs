using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ssg.Core.Networking;

public class NetworkMessageSerializerBinary : NetworkMessageSerializer
{
    public byte[] Serialize(NetworkMessage message)
    {
        var stream = new System.IO.MemoryStream();
        var writer = new System.IO.BinaryWriter(stream);

        var netMsgType = (NetworkMessageType)message.Type;

        writer.Write(message.Type);

        switch (netMsgType)
        {
            case NetworkMessageType.JoinRequest:
                {
                    var msg = message.Data as JoinRequest;
                    writer.Write(msg.m_clientVersion);
                    writer.Write(msg.m_playerName);
                    break;
                }

            case NetworkMessageType.JoinAccept:
                {
                    var msg = message.Data as JoinAccept;
                    break;
                }
        }

        writer.Flush();
        var data = stream.ToArray();
        writer.Close();

        return data;
    }

    public NetworkMessage Deserialize(byte[] data)
    {
        var stream = new System.IO.MemoryStream(data);
        var reader = new System.IO.BinaryReader(stream);

        var message = new NetworkMessage();
        message.Type = reader.ReadInt16();

        switch ((NetworkMessageType)message.Type)
        {
            case NetworkMessageType.JoinRequest:
                {
                    var msg = new JoinRequest();
                    msg.m_clientVersion = reader.ReadInt16();
                    msg.m_playerName = reader.ReadString();
                    message.Data = msg;
                    break;
                }

            case NetworkMessageType.JoinAccept:
                {
                    var msg = new JoinAccept();
                    message.Data = msg;
                    break;
                }
        }

        reader.Close();

        return message;
    }
}
