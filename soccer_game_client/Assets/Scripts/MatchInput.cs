using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchInput
{
    private UserInput m_userInput;
    private PlayerDirection m_direction;
    private PlayerDirection m_prevDirection;

    private bool m_action;
    private bool m_prevAction;
    private float m_actionDuration;

    public MatchInput(UserInput userInput)
    {
        m_userInput = userInput;
    }

    public PlayerDirection GetDirection()
    {
        return m_direction;
    }

    public bool GetAction()
    {
        return !m_action && m_prevAction;
    }

    public float GetActionDuration()
    {
        return m_actionDuration;
    }

    public bool ActionChanged()
    {
        return m_action != m_prevAction;
    }

    public bool DirectionChanged()
    {
        return m_direction != m_prevDirection;
    }

    public void Update(float deltaTime)
    {
        m_prevDirection = m_direction;
        m_direction = m_userInput.GetDirection();

        m_prevAction = m_action;
        m_action = m_userInput.GetAction();

        // Just pressed
        if (!m_prevAction && m_action)
        {
            m_actionDuration = 0.0f;
        }
        // Holding
        else if (m_action)
        {
            m_actionDuration += deltaTime;
        }
    }
}
