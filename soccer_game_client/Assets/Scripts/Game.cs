using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    private static Game m_instance;

    private GameServer m_gameServer;

    public static Game Get()
    {
        if (m_instance == null)
            m_instance = new Game();

        return m_instance;
    }

    public void StartServer()
    {
        if (m_gameServer != null)
        {
            Debug.Assert(false);
            return;
        }

        m_gameServer = new GameServer();
        m_gameServer.StartServer();
    }
}
