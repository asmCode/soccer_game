using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameServerController
{
    private GameServer m_gameServer;

    public GameServerController(GameServer gameServer)
    {
        m_gameServer = gameServer;
    }

    public void StartServer()
    {
        m_gameServer.StartServer();
    }

    public void StopServer()
    {
        m_gameServer.StopServer();
    }

    public void Update(float deltaTime)
    {
        m_gameServer.Update();

        //var message = m_gameServer.GetMessage();
        //if (message == null)
        //    return;
    }
}
