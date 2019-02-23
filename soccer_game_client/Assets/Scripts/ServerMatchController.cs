using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerMatchController : MatchController
{
    private Match m_match;
    private GameServer m_gameServer;

    public ServerMatchController(Match match)
    {
        m_match = match;
        m_gameServer = GameServerFactory.Create();
        m_gameServer.StartServer();
    }

    public void Cleanup()
    {
        m_gameServer.StopServer();
    }

    public void Update(float deltaTime)
    {
        m_gameServer.Update();
        ProcessMessages();
        m_match.Update(deltaTime);
        UpdateClients();
    }

    public void ProcessMessages()
    {
        //while (true)
        //{
        //    var message = m_gameServer.GetMessage();
        //    if (message == null)
        //        break;

        //    m_match.ProcessMessage(message);
        //}
    }

    private void UpdateClients()
    {
        //var players = m_match.GetPlayers();

        //foreach (var player in players)
        //{
        //    var msg = PlayerPosition.Create(player.Team, player.Index, player.GetPosition(), player.GetDirection());
        //    m_gameServer.SendToAll(msg);
        //}

        //var ballPosition = BallPosition.Create(m_match.GetBallPosition());
        //m_gameServer.SendToAll(ballPosition);
    }
}
