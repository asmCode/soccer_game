using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FakeClientOnServer
{
    private MatchInputProcessor m_inputProc;

    private int m_clientMessageNumber;

    public GameClient GameClient
    {
        get;
        private set;
    }

    public FakeClientOnServer()
    {
        m_inputProc = new MatchInputProcessor(new MouseAndKbInput());

        var netCom = GetNetworkCommunication();
        GameClient = new GameClient(netCom);
        GameClient.OpponentFound += HandleOpponentFound;
        GameClient.MatchStarted += HandleMatchStarted;
    }

    public void Update()
    {
        GameClient.Update();

        UpdateInput(Time.deltaTime);
    }

    private void OnDisable()
    {
        GameClient.Disconnect();
    }

    public void SendReadyToStart()
    {
        GameClient.SendReadyToStart();
    }

    private INetworkCommunication GetNetworkCommunication()
    {
        var udpCom = new UdpCommunication(0);
        //return new DelayedNetworkCommunication(udpCom);

        return udpCom;
    }

    private void HandleOpponentFound()
    {
        GameClient.SendReadyToStart();
    }

    private void HandleMatchStarted()
    {
    }

    public void JoinServer()
    {
        GameClient.Join();
    }

    private void UpdateInput(float deltaTime)
    {
        if (m_inputProc == null)
            return;

        m_inputProc.Update(deltaTime);

        var moveMessage = m_inputProc.GetMoveMessage();
        if (moveMessage != null)
        {
            m_clientMessageNumber++;
            moveMessage.m_messageNumber = m_clientMessageNumber;
            SendPlayerMoveMessage(moveMessage);
        }

        float actionDuration;
        if (m_inputProc.GetAction(out actionDuration))
            SendPlayerActionMessage(actionDuration);
    }

    private void SendPlayerMoveMessage(PlayerMove msg)
    {
        GameClient.m_netMsgSerializer.Serialize(msg);
        GameClient.Send(GameClient.m_netMsgSerializer.Data, GameClient.m_netMsgSerializer.DataSize);
    }

    private void SendPlayerActionMessage(float duration)
    {
        var msg = new Action();
        msg.m_duration = duration;
        GameClient.m_netMsgSerializer.Serialize(msg);
        GameClient.Send(GameClient.m_netMsgSerializer.Data, GameClient.m_netMsgSerializer.DataSize);
    }
}
