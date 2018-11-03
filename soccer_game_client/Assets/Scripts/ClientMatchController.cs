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
    }

    public void Update(float deltaTime)
    {
        m_inputCtrl.Update(deltaTime, 0);
        m_gameClient.Update();

        ProcessNetworkMessages();

        while (!m_inputCtrl.MessageQueue.Empty())
            m_gameClient.Send(m_inputCtrl.MessageQueue.Dequeue());
    }

    public void Cleanup()
    {
        m_gameClient.Disconnect();
    }

    public void ProcessNetworkMessages()
    {
        while (true)
        {
            var message = m_gameClient.GetMessage();
            if (message == null)
                break;

            m_match.ProcessMessage(message);
        }
    }
}
