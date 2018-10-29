using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchScene : MonoBehaviour
{
    public bool m_isServer;

    public MatchView m_matchView;

    private Match m_match;
    private MatchController m_matchCtrl;

    private void Awake()
    {
        m_match = new Match();

        if (m_isServer)
        {
            m_matchCtrl = ServerBuilder.Create(m_match);
        }
        else
        {
            m_matchCtrl = ClientBuilder.Create(m_match, m_matchView);
        }
    }

    private void Update()
    {
        m_matchCtrl.Update(Time.deltaTime);
    }

    private void OnApplicationQuit()
    {
        m_matchCtrl.Cleanup();
    }
}
