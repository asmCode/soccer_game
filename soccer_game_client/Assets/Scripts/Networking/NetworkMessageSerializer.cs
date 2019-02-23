using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ssg.Core.Networking;

public class NetworkMessageSerializer
{
    private IDataWriter m_writer;
    private IDataReader m_reader;

    public byte[] Data
    {
        get { return m_writer.Data; }
    }

    public int DataSize
    {
        get { return m_writer.DataSize; }
    }

    public NetworkMessageSerializer(IDataWriter writer, IDataReader reader)
    {
        m_writer = writer;
        m_reader = reader;
    }

    public void Serialize(JoinRequest message)
    {
        m_writer.Reset();
        m_writer.Write(NetworkMessageType.JoinRequest);
        m_writer.Write(message.m_clientVersion);
        m_writer.Write(message.m_playerName);
        m_writer.Flush();
    }

    public void Serialize(JoinAccept message)
    {
        m_writer.Reset();
        m_writer.Write(NetworkMessageType.JoinAccept);
        m_writer.Flush();
    }

    public void Serialize(ReadyToStart message)
    {
        m_writer.Reset();
        m_writer.Write(NetworkMessageType.ReadyToStart);
        m_writer.Flush();
    }

    public void Serialize(OpponentFound message)
    {
        m_writer.Reset();
        m_writer.Write(NetworkMessageType.OpponentFound);
        m_writer.Write(message.m_playerName);
        m_writer.Flush();
    }

    public void Serialize(StartMatch message)
    {
        m_writer.Reset();
        m_writer.Write(NetworkMessageType.StartMatch);
        m_writer.Flush();
    }

    public void Serialize(PlayerMove message)
    {
        // Neither m_team nor m_playerIndex is being sent.

        m_writer.Reset();
        m_writer.Write(NetworkMessageType.PlayerMove);
        m_writer.Write(message.m_dt);
        m_writer.Write((byte)message.m_playerDirection);
        m_writer.Flush();
    }

    public NetworkMessage Deserialize(byte[] data, int size)
    {
        var message = new NetworkMessage();
        m_reader.Reset(data, size);
        m_reader.Read(out message.m_type);

        switch (message.m_type)
        {
            case NetworkMessageType.JoinRequest:
                {
                    var msg = new JoinRequest();
                    m_reader.Read(out msg.m_clientVersion);
                    m_reader.Read(out msg.m_playerName);
                    message.m_msg = msg;
                    break;
                }

            case NetworkMessageType.JoinAccept:
                break;

            case NetworkMessageType.OpponentFound:
                {
                    var msg = new OpponentFound();
                    m_reader.Read(out msg.m_playerName);
                    message.m_msg = msg;
                    break;
                }

            case NetworkMessageType.ReadyToStart:
                break;

            case NetworkMessageType.StartMatch:
                break;

            case NetworkMessageType.PlayerMove:
                {
                    var msg = new PlayerMove();
                    m_reader.Read(out msg.m_dt);
                    byte direction;
                    m_reader.Read(out direction);
                    msg.m_playerDirection = (PlayerDirection)direction;
                    message.m_msg = msg;
                    break;
                }

            default:
                Debug.Log("Unknown network message.");
                break;
        }

        return message;
    }
}
