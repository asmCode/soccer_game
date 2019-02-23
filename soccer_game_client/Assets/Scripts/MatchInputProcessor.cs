using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchInputProcessor
{
    private UserInput m_userInput;
    private bool m_prevActionState;
    private float m_actionDuration;

    public MatchInputProcessor(UserInput userInput)
    {
        m_userInput = userInput;
    }

    public MatchMessage GetMoveMessage()
    {
        return MovePlayer.Create(0, 0, 0, m_userInput.GetDirection());
    }

    public void Update(float deltaTime)
    {
        m_userInput.GetDirection();

        //bool actionState = m_userInput.GetAction();

        //if (!m_prevActionState && actionState)
        //    m_actionDuration = 0.0f;

        //if (actionState)
        //    m_actionDuration += deltaTime;

        //m_prevActionState = actionState;
    }
}
