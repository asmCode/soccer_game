using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMatchController : MatchController
{
    private InputController m_inputCtrl;
    private Match m_match;
    
    public LocalMatchController(Match match, UserInput userInput)
    {
        m_match = match;
        m_inputCtrl = new InputController(userInput, 0);

        var initialData = new PlayersProvider();
        var players = initialData.GetPlayers();

        m_match.SetPlayers(players);
    }

    public void Update(float deltaTime)
    {
        m_inputCtrl.Update(deltaTime, 0);

        while (!m_inputCtrl.MessageQueue.Empty())
        {
            var localMessage = m_inputCtrl.MessageQueue.Dequeue();
            m_match.ProcessMessage(localMessage);
        }
    }

    public void Cleanup()
    {
    }
}
