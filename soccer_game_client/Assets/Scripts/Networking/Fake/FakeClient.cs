using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is fake client to be used on the true Server scene. This class pretends real client.
public class FakeClient
{
    private UserInput m_userInput;
    private MatchInputProcessor m_inputProc;
    private string m_playerName;
    private NetworkMessageSerializer m_netMsgSerializer = new NetworkMessageSerializer(new BinaryDataWriter(), new BinaryDataReader());

    public Queue<RawData> OutMessages
    {
        get;
        private set;
    }

    public INetworkAddress NetworkAddress
    {
        get;
        private set;
    }

    public FakeClient(UserInput input, string playerName, string netAddress)
    {
        m_userInput = input;
        OutMessages = new Queue<RawData>();
        m_playerName = playerName;

        NetworkAddress = new FakeAddress(netAddress);

        if (m_userInput != null)
            m_inputProc = new MatchInputProcessor(m_userInput);
    }

    public void SendJoinRequest()
    {
        var msg = new JoinRequest();
        msg.m_playerName = m_playerName;
        msg.m_clientVersion = 1;
        m_netMsgSerializer.Serialize(msg);

        OutMessages.Enqueue(new RawData(m_netMsgSerializer.Data, m_netMsgSerializer.DataSize));
    }

    public void SendReadyToStart()
    {
        var msg = new ReadyToStart();
        m_netMsgSerializer.Serialize(msg);

        OutMessages.Enqueue(new RawData(m_netMsgSerializer.Data, m_netMsgSerializer.DataSize));
    }

    private void SendPlayerMoveMessage(PlayerMove msg)
    {
        m_netMsgSerializer.Serialize(msg);
        OutMessages.Enqueue(new RawData(m_netMsgSerializer.Data, m_netMsgSerializer.DataSize));
    }

    public void Update(float deltaTime)
    {
        UpdateInput(deltaTime);
    }

    private void UpdateInput(float deltaTime)
    {
        if (m_inputProc == null)
            return;

        m_inputProc.Update(deltaTime);

        var moveMessage = m_inputProc.GetMoveMessage();
        if (moveMessage != null)
            SendPlayerMoveMessage(moveMessage);
    }
}
