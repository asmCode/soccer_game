using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController
{
    private UserInput m_input;
    private byte m_team;

    public MessageQueue MessageQueue { get; private set; }

    public InputController(UserInput input, byte team)
    {
        m_input = input;
        m_team = team;

        MessageQueue = new MessageQueue();
    }

    public void Update(float deltaTime, byte playerIndex)
    {
        MessageQueue.Clear();

        var direction = m_input.GetDirection();

        if (direction != PlayerDirection.None)
        {
            MessageQueue.AddMessage(MovePlayer.Create(deltaTime, m_team, playerIndex, direction));
        }
    }
}
