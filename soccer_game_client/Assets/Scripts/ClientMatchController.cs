using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientMatchController : MatchController
{
    private InputController m_inputCtrl;
    private Match m_match;
    private NetworkController m_netCtrl;

    public ClientMatchController(Match match, UserInput userInput)
    {
        m_match = match;
        m_inputCtrl = new InputController(userInput, 0);
        m_netCtrl = new NetworkController();
    }

    public void Update(float deltaTime)
    {
        m_inputCtrl.Update(deltaTime, 0);
        m_netCtrl.Update();

        while (!m_inputCtrl.MessageQueue.Empty())
            m_netCtrl.SendMessage(m_inputCtrl.MessageQueue.Dequeue());

        while (!m_netCtrl.MessageQueue.Empty())
            ProcessMessage(deltaTime, m_netCtrl.MessageQueue.Dequeue());
    }

    public void Cleanup()
    {
        m_netCtrl.Cleanup();
    }

    private void ProcessMessage(float deltaTime, Message msg)
    {
        switch (msg.m_messageType)
        {
            case MessageType.PlayerMove:
                var playerMoveMsg = msg.m_message as MovePlayer;
                var moveVector = PlayerDirectionVector.GetVector(playerMoveMsg.m_playerDirection);
                m_match.SetPlayerPosition(0, 0, m_match.GetPlayerPosition(0, 0) + moveVector * playerMoveMsg.m_dt);
                break;
        }
    }
}
