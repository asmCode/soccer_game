using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchController
{
    private InputController m_inputCtrl;
    private Match m_match;
    private NetworkController m_netCtrl;

    public MatchController(Match match, UserInput userInput)
    {
        m_match = match;
        m_inputCtrl = new InputController(userInput, 0);
        m_netCtrl = new NetworkController();
    }

    public void Update(float deltaTime)
    {
        m_inputCtrl.Update(deltaTime, 0);

        while (!m_inputCtrl.MessageQueue.Empty())
            m_netCtrl.SendMessage(m_inputCtrl.MessageQueue.Dequeue());

        while (!m_netCtrl.MessageQueue.Empty())
            ProcessMessage(deltaTime, m_netCtrl.MessageQueue.Dequeue());
    }

    private void ProcessMessage(float deltaTime, Message msg)
    {
        switch (msg.m_messageType)
        {
            case MessageType.PlayerMove:
                var playerMoveMsg = msg.m_message as MovePlayer;
                var direction = playerMoveMsg.m_playerDirection;

                if (direction == PlayerDirection.Up)
                {
                    m_match.SetPlayerPosition(0, 0, m_match.GetPlayerPosition(0, 0) + Vector3.forward * deltaTime);
                }
                break;
        }
    }
}
