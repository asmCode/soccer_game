using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchScene : MonoBehaviour
{
    public UserInput m_userInput;

    private Match m_match;
    private MatchController m_matchCtrl;

    private void Awake()
    {
        m_match = new Match();
        m_matchCtrl = new MatchController(m_match, m_userInput);
    }

    private void Update()
    {
        m_matchCtrl.Update(Time.deltaTime);
    }
}
