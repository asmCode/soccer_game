using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchScene : MonoBehaviour
{
    public MatchControlType m_matchControlType;

    public MatchView m_matchView;

    private Match m_match;
    private MatchController m_matchCtrl;

    private void Awake()
    {
        m_match = new Match();

        switch (m_matchControlType)
        {
            case MatchControlType.Local:
                {
                    var userInput = new MouseAndKbInput();
                    m_matchCtrl = new LocalMatchController(m_match, userInput);
                    m_matchView.Init(m_match);
                    break;
                }

            case MatchControlType.Client:
                {
                    m_match.SetLogic(new ClientMatchLogic(m_match));
                    m_matchCtrl = ClientBuilder.Create(m_match, m_matchView);
                    break;
                }

            case MatchControlType.Server:
                {
                    m_match.SetLogic(new ServerMatchLogic(m_match));
                    m_matchCtrl = ServerBuilder.Create(m_match);
                    m_matchView.Init(m_match);
                    break;
                }
        }
    }

    private void Update()
    {
        if (m_matchCtrl == null)
            return;

        m_matchCtrl.Update(Time.deltaTime);
    }

    private void OnApplicationQuit()
    {
        m_matchCtrl.Cleanup();
    }
}
