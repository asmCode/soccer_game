using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match
{
    private Team[] m_teams = new Team[2];
    private MessageInterpreter m_messageInterpreter;

    public Match()
    {
        m_messageInterpreter = new MessageInterpreter();

        m_teams[0] = new Team();
        m_teams[1] = new Team();

        for (byte i = 0; i < Team.PlayerCount; i++)
        {
            m_teams[0].Players[i] = new Player(0, i, Vector3.zero);
            m_teams[1].Players[i] = new Player(1, i, Vector3.zero);
        }
    }

    public Vector3 GetPlayerPosition(byte team, byte playerIndex)
    {
        return m_teams[team].Players[playerIndex].Position;
    }

    public void SetPlayerPosition(byte team, byte playerIndex, Vector3 position)
    {
        m_teams[team].Players[playerIndex].Position = position;
    }

    public void ProcessMessage(Message message)
    {
        m_messageInterpreter.ProcessMessage(this, message);
    }
}
