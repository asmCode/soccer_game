using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchInputProcessor
{
    private UserInput m_userInput;
    private bool m_prevActionState;
    private float m_actionDuration;
    private bool m_hasAction;

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

    public bool GetAction(out float duration)
    {
        duration = m_actionDuration;
        return m_hasAction;
    }

    public void Update(float deltaTime)
    {
        m_deltaTime = deltaTime;
        m_userInput.GetDirection();

        bool actionState = m_userInput.GetAction();

        // Just pressed
        if (!m_prevActionState && actionState)
        {
            m_hasAction = false;
            m_actionDuration = 0.0f;
        }
        // Just released
        if (m_prevActionState && !actionState)
        {
            m_hasAction = true;
        }
        // Holding
        else if (actionState)
        {
            m_actionDuration += deltaTime;
        }
        // Actions is not pressed
        else
        {
            m_actionDuration += deltaTime;
            m_hasAction = false;
        }

        m_prevActionState = actionState;
    }
}
