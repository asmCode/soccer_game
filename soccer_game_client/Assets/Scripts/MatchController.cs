using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchController
{
    private UserInput m_userInput;
    private Match m_match;

    public MatchController(Match match, UserInput userInput)
    {
        m_match = match;
        m_userInput = userInput;
    }
  
    public void Update(float deltaTime)
    {
        if (m_userInput.Direction != PlayerDirection.None)
        {
            m_match.MovePlayer(0, 0, m_userInput.Direction);
        }
    }
}
