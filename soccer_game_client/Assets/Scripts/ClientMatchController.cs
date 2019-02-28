using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientMatchController : MatchController
{
    private InputController m_inputCtrl;
    private Match m_match;
    private GameClient m_gameClient;

    public ClientMatchController(Match match, UserInput userInput)
    {
        m_match = match;
        m_inputCtrl = new InputController(userInput, 0);
        m_gameClient = GameClientFactory.Create();

        var initialData = new PlayersProvider();
        var players = initialData.GetPlayers();
        m_match.SetPlayers(players);
    }

    public void Update(float deltaTime)
    {
        m_gameClient.Update();
        if (!m_gameClient.IsConnected())
            return;

        m_inputCtrl.Update(deltaTime, 0);
        ProcessNetworkMessages();

        while (!m_inputCtrl.MessageQueue.Empty())
        {
            var localMessage = m_inputCtrl.MessageQueue.Dequeue();
            // m_gameClient.Send(localMessage);
            // m_match.ProcessMessage(localMessage);
        }

        m_match.Update(deltaTime);
    }

    public void Cleanup()
    {
        m_gameClient.Disconnect();
    }

    public void ProcessNetworkMessages()
    {
        while (true)
        {
            var message = m_gameClient.GetMatchMessage();
            if (message == null)
                break;

            m_match.ProcessMessage(message);
        }
    }
}
