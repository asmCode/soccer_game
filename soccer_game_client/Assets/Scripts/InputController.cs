using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController
{
    private UserInput m_input;
    private byte m_team;
    private bool m_prevActionState;
    private float m_actionStartTime;

    public MessageQueue MessageQueue { get; private set; }

    public InputController(UserInput input, byte team)
    {
        m_input = input;
        m_team = team;

        MessageQueue = new MessageQueue();
    }

    public void Update(float deltaTime, byte playerIndex)
    {
        MessageQueue.Clear();

        var direction = m_input.GetDirection();

        if (direction != PlayerDirection.None)
        {
            MessageQueue.AddMessage(PlayerMove.Create(deltaTime, m_team, playerIndex, direction));
        }

        if (m_prevActionState != m_input.GetAction())
        {
            if (m_input.GetAction())
            {
                m_actionStartTime = Time.time;
            }
            else
            {
                var actionMsg = new Action();
                actionMsg.m_team = m_team;
                actionMsg.m_duration = Time.time - m_actionStartTime;

                var msg = new MatchMessage();
                msg.m_messageType = MessageType.PlayerAction;
                msg.m_message = actionMsg;

                MessageQueue.AddMessage(msg);
            }

            m_prevActionState = m_input.GetAction();
        }
    }
}
