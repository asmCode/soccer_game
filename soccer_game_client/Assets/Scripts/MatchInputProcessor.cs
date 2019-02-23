using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchInputProcessor
{
    private UserInput m_userInput;
    private bool m_prevActionState;
    private float m_actionDuration;

    private float m_deltaTime;

    public MatchInputProcessor(UserInput userInput)
    {
        m_userInput = userInput;
    }

    public PlayerMove GetMoveMessage()
    {
        if (m_userInput.GetDirection() == PlayerDirection.None)
            return null;

        var msg = new PlayerMove();
        msg.m_dt = m_deltaTime;
        msg.m_playerDirection = m_userInput.GetDirection();
        return msg;
    }

    public void Update(float deltaTime)
    {
        m_deltaTime = deltaTime;
        m_userInput.GetDirection();

        //bool actionState = m_userInput.GetAction();

        //if (!m_prevActionState && actionState)
        //    m_actionDuration = 0.0f;

        //if (actionState)
        //    m_actionDuration += deltaTime;

        //m_prevActionState = actionState;
    }
}
