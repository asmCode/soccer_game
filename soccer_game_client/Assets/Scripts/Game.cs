using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static Game m_instance;

    private GameServer m_gameServer;

    public static Game Get()
    {
        if (m_instance == null)
            m_instance = GameObject.Find("Game").GetComponent<Game>();

        return m_instance;
    }

    public void StartServer()
    {
        if (m_gameServer != null)
        {
            Debug.Assert(false);
            return;
        }

        m_gameServer = new GameServer(null);
        m_gameServer.StartServer();
    }

    // public void 
}
